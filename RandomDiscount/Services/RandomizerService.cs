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

            // black magic with random to give it more spread
            int result = 0;
            if (factor == 0) factor = 1;
            for (int i = 0; i < max; i++) {
                result = randomizer.Next(min, max + 1);
                var percent = max / 100f;
                var vll = result / percent;
                if (vll < factor) break;
            }
            
            return result;
        }
    }
}
