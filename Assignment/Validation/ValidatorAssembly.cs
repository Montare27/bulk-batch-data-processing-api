namespace Assignment.Validation
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ValidatorAssembly
    {
        public static bool Validate<T>(T item) where T : class
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            var types = assembly.GetTypes()
                .Where(x=> x.GetInterfaces()
                    .Any(i => 
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                if (instance is IValidator<T> correctType)
                {
                    var method = type.GetMethod("Validate");
                    var result = method?.Invoke(correctType, new object[] {item});

                    if (result != null)
                    {
                        return (bool)result;
                    }
                }
            }

            return false;
        }
    }
}
