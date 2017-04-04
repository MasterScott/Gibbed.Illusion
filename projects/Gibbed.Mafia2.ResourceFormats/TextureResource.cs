﻿/* Copyright (c) 2017 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Mafia2.ResourceFormats
{
    public class TextureResource : IResourceType
    {
        public ulong NameHash;
        public byte Unknown8;
        public byte Unknown9;
        public byte[] Data;

        public void Serialize(ushort version, Stream output, Endian endian)
        {
            output.WriteValueU64(this.NameHash, endian);
            output.WriteValueU8(this.Unknown8);
            if (version == 2)
            {
                output.WriteValueU8(this.Unknown9);
            }
            output.WriteBytes(this.Data);
        }

        public void Deserialize(ushort version, Stream input, Endian endian)
        {
            this.NameHash = input.ReadValueU64(endian);
            this.Unknown8 = input.ReadValueU8();
            this.Unknown9 = version == 2 ? input.ReadValueU8() : (byte)0;

            if (this.Unknown9 != 0 && this.Unknown9 != 1)
            {
                throw new InvalidOperationException();
            }

            this.Data = input.ReadBytes((uint)(input.Length - input.Position));
        }
    }
}
