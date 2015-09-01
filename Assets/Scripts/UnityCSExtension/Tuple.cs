using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityCSExtension {
    public class Tuple<T1, T2> {
        public T1 First;
        public T2 Second;

        public Tuple(T1 First, T2 Second) {
            this.First = First;
            this.Second = Second;
        }

        public override string ToString() {
            return string.Format("<{0}, {1}>", First, Second);
        }
    }
}