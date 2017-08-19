using System;

namespace RandomDiscount.Ui {
    public static class ObjectExtensions {
        public static void Let<T>(this T self, Action<T> block){
            block(self);
        }
    }
}
