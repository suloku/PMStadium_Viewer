/*
 * Created by SharpDevelop.
 * User: sergi
 * Date: 08/02/2017
 * Time: 0:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

namespace Stadium_Viewer
{
	/// <summary>
	/// Description of jpk1.
	/// </summary>
	public class jpk1
	{
		public jpk1()
		{
		}
		public int Size = 44;
		public byte[] Data;
	    public jpk1(byte[] data = null)
	    {
	    	Data = data ?? new byte[Size];
	    }
		
	    
	    //Quite riped from pkhex
		public int Species
        {
            get { return Data[0]; }
            set
            {
            	Data[0] = (byte)value;
            }
        }

	    public int Stat_HPCurrent { get { return BigEndian.ToUInt16(Data, 0x1); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x1); } }
        public int Status_Condition { get { return Data[4]; } set { Data[4] = (byte)value; } }
        public int Type_A { get { return Data[5]; } set { Data[5] = (byte)value; } }
        public int Type_B { get { return Data[6]; } set { Data[6] = (byte)value; } }
        public int Catch_Rate { get { return Data[7]; } set { Data[7] = (byte)value; } }
        public int Move1 { get { return Data[8]; } set { Data[8] = (byte) value; } }
        public int Move2 { get { return Data[9]; } set { Data[9] = (byte)value; } }
        public int Move3 { get { return Data[10]; } set { Data[10] = (byte)value; } }
        public int Move4 { get { return Data[11]; } set { Data[11] = (byte)value; } }
        public int TID { get { return BigEndian.ToUInt16(Data, 0xC); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0xC); } }
        public uint EXP
        {
            get { return (BigEndian.ToUInt32(Data, 0xE) >> 8) & 0x00FFFFFF; }
            set { Array.Copy(BigEndian.GetBytes((value << 8) & 0xFFFFFF00), 0, Data, 0xE, 3); }
        }
        public int EV_HP { get { return BigEndian.ToUInt16(Data, 0x11); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x11); } }
        public int EV_ATK { get { return BigEndian.ToUInt16(Data, 0x13); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x13); } }
        public int EV_DEF { get { return BigEndian.ToUInt16(Data, 0x15); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x15); } }
        public int EV_SPE { get { return BigEndian.ToUInt16(Data, 0x17); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x17); } }
        public int EV_SPC { get { return BigEndian.ToUInt16(Data, 0x19); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x19); } }
        public int EV_SPA { get { return EV_SPC; } set { EV_SPC = value; } }
        public int EV_SPD { get { return EV_SPC; } set { } }
        public ushort DV16 { get { return BigEndian.ToUInt16(Data, 0x1B); } set { BigEndian.GetBytes(value).CopyTo(Data, 0x1B); } }
        public int IV_HP { get { return ((IV_ATK & 1) << 3) | ((IV_DEF & 1) << 2) | ((IV_SPE & 1) << 1) | ((IV_SPC & 1) << 0); } set { } }
        public int IV_ATK { get { return (DV16 >> 12) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 12)) | (ushort)((value > 0xF ? 0xF : value) << 12)); } }
        public int IV_DEF { get { return (DV16 >> 8) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 8)) | (ushort)((value > 0xF ? 0xF : value) << 8)); } }
        public int IV_SPE { get { return (DV16 >> 4) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 4)) | (ushort)((value > 0xF ? 0xF : value) << 4)); } }
        public int IV_SPC { get { return (DV16 >> 0) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 0)) | (ushort)((value > 0xF ? 0xF : value) << 0)); } }
        public int IV_SPA { get { return IV_SPC; } set { IV_SPC = value; } }
        public int IV_SPD { get { return IV_SPC; } set { } }
        public int Move1_PP { get { return Data[0x1D] & 0x3F; } set { Data[0x1D] = (byte)((Data[0x1D] & 0xC0) | (value & 0x3F)); } }
        public int Move2_PP { get { return Data[0x1E] & 0x3F; } set { Data[0x1E] = (byte)((Data[0x1E] & 0xC0) | (value & 0x3F)); } }
        public int Move3_PP { get { return Data[0x1F] & 0x3F; } set { Data[0x1F] = (byte)((Data[0x1F] & 0xC0) | (value & 0x3F)); } }
        public int Move4_PP { get { return Data[0x20] & 0x3F; } set { Data[0x20] = (byte)((Data[0x20] & 0xC0) | (value & 0x3F)); } }
        public int Move1_PPUps { get { return (Data[0x1D] & 0xC0) >> 6; } set { Data[0x1D] = (byte)((Data[0x1D] & 0x3F) | ((value & 0x3) << 6)); } }
        public int Move2_PPUps { get { return (Data[0x1E] & 0xC0) >> 6; } set { Data[0x1E] = (byte)((Data[0x1E] & 0x3F) | ((value & 0x3) << 6)); } }
        public int Move3_PPUps { get { return (Data[0x1F] & 0xC0) >> 6; } set { Data[0x1F] = (byte)((Data[0x1F] & 0x3F) | ((value & 0x3) << 6)); } }
        public int Move4_PPUps { get { return (Data[0x20] & 0xC0) >> 6; } set { Data[0x20] = (byte)((Data[0x20] & 0x3F) | ((value & 0x3) << 6)); } }

        public int Stat_Level
        {
            get { return Data[0x21]; }
            set { Data[0x21] = (byte)value; Data[0x3] = (byte)value; }
        }
        public int Stat_HPMax { get { return BigEndian.ToUInt16(Data, 0x22); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x22); } }
        public int Stat_ATK { get { return BigEndian.ToUInt16(Data, 0x24); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x24); } }
        public int Stat_DEF { get { return BigEndian.ToUInt16(Data, 0x26); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x26); } }
        public int Stat_SPE { get { return BigEndian.ToUInt16(Data, 0x28); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x28); } }
        public int Stat_SPC { get { return BigEndian.ToUInt16(Data, 0x2A); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x2A); } }
        // Leave SPA and SPD as alias for SPC
        public int Stat_SPA { get { return Stat_SPC; } set { Stat_SPC = value; } }
        public int Stat_SPD { get { return Stat_SPC; } set { } }

	    
	}
}
