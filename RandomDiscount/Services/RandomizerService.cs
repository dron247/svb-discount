using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDiscount.Services {
    class RandomizerService {
        private Random randomizer;
        Properties.Settings settings;

        public RandomizerService(Properties.Settings settings) {
            randomizer = new Random(3);
            this.settings = settings;
        }

        public int Result {
            get {
                return CalculateRandom(
                    randomizer,
                    settings.MinDiscount,
                    settings.MaxDiscount,
                    settings.DiscountFactor
                );
            }
        }


        private int CalculateRandom(Random randomizer, int min, int max, int factor) {
            
            if (factor >= 100) {
                return max;
            }

            if (factor <= 0) {
                return min;
            }

            var range = Enumerable.Range(min, max - min);
            var index = randomizer.Next(range.Count() - 1);


            return range.ToArray()[index];
        }
    }
}
