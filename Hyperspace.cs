// The MIT License (MIT)
// 
// Copyright (c) 2018 Sarbian
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Hyperspace
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    class Hyperspace : MonoBehaviour
    {
        void Awake()
        {
            // Fetch the original constructor
            ConstructorInfo brpc = typeof(BinaryReader).GetConstructor(new[] { typeof(Stream) });

            // Fetch our replacement
            ConstructorInfo ftpc = typeof(HyperspaceReader).GetConstructor(new[] { typeof(Stream) });

            // Redirect the original method to the new one
            if ( brpc != null && ftpc!= null && Detourer.TryDetourFromTo(brpc, ftpc))
            {
                Debug.Log("Hyperspace activated");
            }
        }
    }

    public class HyperspaceReader : BinaryReader
    {
        public HyperspaceReader(Stream input) : this(new BufferedStream(input, 4096 * 4), new UTF8Encoding())
        {
        }

        public HyperspaceReader(Stream input, Encoding encoding) : base(new BufferedStream(input, 4096 * 4), encoding)
        {
        }
    }
}
