using Bogus.DataSets;
using System.Collections.Generic;
using Bogus;
using MsBlazorServerPlayGround.Models;

namespace MsBlazorServerPlayGround.Data
{
    public static class VirtualizationDataFactory
    {
        private static readonly Lorem loremIpsum;
        private static readonly Randomizer random;
        private static readonly Person person;

        static VirtualizationDataFactory()
        {
            loremIpsum = new();
            random = new();
            person = new();
        }

        public static IEnumerable<SomePerson> GenerateData(int amount = 100)
        {
            IList<SomePerson> people = new List<SomePerson>(amount);
            for (var i = 0; i < amount; i++)
            {
                people.Add(new SomePerson
                {
                     Id = random.Int(0, amount)
                    ,FirstName = person.FirstName
                    ,LastName = person.LastName
                    ,Bio = loremIpsum.Paragraphs(random.Int(1, 3))
                });
            }
            return people;
        }
    }
}
