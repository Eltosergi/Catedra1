using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Catedra_1.src.Models;

namespace Catedra_1.src.Data
{
    public class DataSeeders
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {   
                Dictionary<string, string> diccionario = new Dictionary<string, string>();
                diccionario.Add("Male", "masculino");
                diccionario.Add("Female", "femenino");
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDBContext>();

                var existingRuts = new HashSet<string>();

                if (!context.Users.Any())
                {
                    var UserFaker = new Faker<User>()
                        .RuleFor(u => u.Rut, f => GenerateUniqueRandomRut(existingRuts))
                        .RuleFor(p => p.Name, f => f.Person.FullName)
                        .RuleFor(p => p.Email, f => f.Person.Email)
                        .RuleFor(p => p.Gender, f => diccionario[f.Person.Gender.ToString()])
                        .RuleFor(p => p.BirthDay, f => f.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd"));
                                    
                    var products = UserFaker.Generate(10);
                    context.Users.AddRange(products);
                    context.SaveChanges();
                }
            }
        }

        private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
        {
            string rut;
            do
            {
                rut = GenerateRandomRut();
            } while (existingRuts.Contains(rut));

            existingRuts.Add(rut);
            return rut;
        }

        private static string GenerateRandomRut()
        {
            Random random = new();
            int rutNumber = random.Next(1, 99999999); // Número aleatorio de 7 o 8 dígitos
            int verificator = CalculateRutVerification(rutNumber);
            string verificatorStr = verificator.ToString();
            if(verificator == 10){
                verificatorStr = "k";
            }

            return $"{rutNumber}-{verificatorStr}";
        }

        private static int CalculateRutVerification(int rutNumber)
        {
            int[] coefficients = { 2, 3, 4, 5, 6, 7 };
            int sum = 0;
            int index = 0;

            while (rutNumber != 0)
            {
                sum += rutNumber % 10 * coefficients[index];
                rutNumber /= 10;
                index = (index + 1) % 6;
            }

            int verification = 11 - (sum % 11);
            return verification == 11 ? 0 : verification;
        }
    }
}