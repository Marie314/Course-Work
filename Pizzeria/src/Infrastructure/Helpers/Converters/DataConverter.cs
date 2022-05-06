using System.Data;
using System.Reflection;

namespace Pizzeria.Infrastructure.Helpers.Converters
{
    /// <summary>
    /// Represent simple conversion of tabular data to ojects.
    /// </summary>
    /// <typeparam name="T">The object into which data from the table is converted.</typeparam>
    internal class DataConverter<T>
        where T : class, new()
    {
        private readonly IDictionary<string, PropertyInfo> _propertiesDictionary;

        public DataConverter(IDataReader reader)
        {
            Reader = reader;
            _propertiesDictionary = GetProperties();
        }

        /// <summary>
        /// Table data reader.
        /// </summary>
        public IDataReader Reader { get; private set; }

        /// <summary>
        /// Returns a set of objects, matching the specified query,
        /// otherwise, is null returned.
        /// </summary>
        public IQueryable<T> GetObjects()
        {
            List<T> result = new List<T>();

            if (Reader.Read())
            {
                T obj;

                do
                {
                    obj = new T();
                    SetObjectProperties(obj);

                    result.Add(obj);
                }
                while (Reader.Read());
            }

            return result.AsQueryable();
        }

        /// <summary>
        /// Returns an objects, matching the specified query,
        /// otherwise, is null returned.
        /// </summary>
        public T GetObject()
        {
            T obj = null;

            while (Reader.Read())
            {
                obj = new T();
                SetObjectProperties(obj);
            }

            return obj;
        }

        private IDictionary<string, PropertyInfo> GetProperties()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var propertyDictionary = new Dictionary<string, PropertyInfo>();

            properties.ForEach(prop =>
            {
                if (prop.CanWrite)
                {
                    propertyDictionary.Add(prop.Name, prop);
                }
            });

            return propertyDictionary;
        }

        private void SetObjectProperties(T obj)
        {
            PropertyInfo property;

            for (var i = 0; i < Reader.FieldCount; i++)
            {
                var key = Reader.GetName(i);
                if (!_propertiesDictionary.TryGetValue(key, out property))
                {
                    continue;
                }

                var value = Reader[i];

                property.SetValue(obj, value, null);
            }
        }
    }
}
