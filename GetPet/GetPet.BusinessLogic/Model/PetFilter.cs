using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetPet.BusinessLogic.Model
{
    public class PetFilter : BaseFilter
    {
        public IEnumerable<int> AnimalTypes { get; set; }

        public IDictionary<int, IEnumerable<int>> TraitValues { get; set; }

        public IEnumerable<int> BooleanTraits { get; set; }

        public DateTime? CreatedSince { get; set; } = null;

        public override bool Equals(object obj) => Equals(obj as PetFilter);

        public override string ToString() => JsonConvert.SerializeObject(new
        {
            AnimalTypes = AnimalTypes.OrderBy(i => i),
            TraitValues = TraitValues.OrderBy(i => i.Key).ToDictionary(i => i, i => i.Value.OrderBy(j => j)),
            BooleanTraits = BooleanTraits.OrderBy(i => i)
        });

        public bool Equals(PetFilter other) => other != null &&
                ToString() == other.ToString();

        public override int GetHashCode() => ToString().GetHashCode();
    }
}