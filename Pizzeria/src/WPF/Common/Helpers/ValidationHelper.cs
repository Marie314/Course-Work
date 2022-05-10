using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WPF.Common.Helpers
{
    internal class ValidationHelper
    {
        public static bool Validate<T>(T instance) =>
            !instance.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ValidationAttribute)))
                .Select(prop =>
                {
                    bool result;

                    try
                    {
                        ValidateProperty(prop.GetValue(instance, null), prop.Name, instance);
                        result = true;
                    }
                    catch (ValidationException)
                    {
                        result = false;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }

                    return result;
                })
                .Any(val => !val);

        public static void ValidateProperty<T>(T value, string name, object instance)
        {
            Validator.ValidateProperty(value, new ValidationContext(instance, null, null)
            {
                MemberName = name,
            });
        }
    }
}
