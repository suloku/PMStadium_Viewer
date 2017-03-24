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
using System.Windows.Forms;

namespace Stadium_Viewer
{
	/// <summary>
	/// Description of js1pk.
	/// </summary>
	public class js1pk
	{
		public js1pk()
		{
		}
		public static int Size = 80;
		public byte[] Data;
	    public js1pk(byte[] data = null)
	    {
	    	Data = data ?? new byte[Size];
	    	
	    	init_table();
	    	init_speciestable();
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

	    public int Stat_HPCurrent { get { return BigEndian.ToUInt16(Data, 0x2); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x2); } }
        public int Status_Condition { get { return Data[5]; } set { Data[5] = (byte)value; } }
        public int Type_A { get { return Data[6]; } set { Data[6] = (byte)value; } }
        public int Type_B { get { return Data[7]; } set { Data[7] = (byte)value; } }
        public int Catch_Rate { get { return Data[8]; } set { Data[8] = (byte)value; } }
        public int Move1 { get { return Data[9]; } set { Data[9] = (byte) value; } }
        public int Move2 { get { return Data[10]; } set { Data[10] = (byte)value; } }
        public int Move3 { get { return Data[11]; } set { Data[11] = (byte)value; } }
        public int Move4 { get { return Data[12]; } set { Data[12] = (byte)value; } }
        public int TID { get { return BigEndian.ToUInt16(Data, 0xE); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0xE); } }
        public uint EXP
        {
            get { return (BigEndian.ToUInt32(Data, 0x11) >> 8) & 0x00FFFFFF; }
            set { Array.Copy(BigEndian.GetBytes((value << 8) & 0xFFFFFF00), 0, Data, 0x11, 3); }
        }
        public int EV_HP { get { return BigEndian.ToUInt16(Data, 0x14); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x14); } }
        public int EV_ATK { get { return BigEndian.ToUInt16(Data, 0x16); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x16); } }
        public int EV_DEF { get { return BigEndian.ToUInt16(Data, 0x18); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x18); } }
        public int EV_SPE { get { return BigEndian.ToUInt16(Data, 0x1A); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x1A); } }
        public int EV_SPC { get { return BigEndian.ToUInt16(Data, 0x1C); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x1C); } }
        public int EV_SPA { get { return EV_SPC; } set { EV_SPC = value; } }
        public int EV_SPD { get { return EV_SPC; } set { } }
        public ushort DV16 { get { return BigEndian.ToUInt16(Data, 0x1E); } set { BigEndian.GetBytes(value).CopyTo(Data, 0x1E); } }
        public int IV_HP { get { return ((IV_ATK & 1) << 3) | ((IV_DEF & 1) << 2) | ((IV_SPE & 1) << 1) | ((IV_SPC & 1) << 0); } set { } }
        public int IV_ATK { get { return (DV16 >> 12) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 12)) | (ushort)((value > 0xF ? 0xF : value) << 12)); } }
        public int IV_DEF { get { return (DV16 >> 8) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 8)) | (ushort)((value > 0xF ? 0xF : value) << 8)); } }
        public int IV_SPE { get { return (DV16 >> 4) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 4)) | (ushort)((value > 0xF ? 0xF : value) << 4)); } }
        public int IV_SPC { get { return (DV16 >> 0) & 0xF; } set { DV16 = (ushort)((DV16 & ~(0xF << 0)) | (ushort)((value > 0xF ? 0xF : value) << 0)); } }
        public int IV_SPA { get { return IV_SPC; } set { IV_SPC = value; } }
        public int IV_SPD { get { return IV_SPC; } set { } }
        public int Move1_PP { get { return Data[0x20] & 0x3F; } set { Data[0x20] = (byte)((Data[0x1D] & 0xC0) | (value & 0x3F)); } }
        public int Move2_PP { get { return Data[0x21] & 0x3F; } set { Data[0x21] = (byte)((Data[0x1E] & 0xC0) | (value & 0x3F)); } }
        public int Move3_PP { get { return Data[0x22] & 0x3F; } set { Data[0x22] = (byte)((Data[0x1F] & 0xC0) | (value & 0x3F)); } }
        public int Move4_PP { get { return Data[0x23] & 0x3F; } set { Data[0x23] = (byte)((Data[0x20] & 0xC0) | (value & 0x3F)); } }
        public int Move1_PPUps { get { return (Data[0x20] & 0xC0) >> 6; } set { Data[0x20] = (byte)((Data[0x1D] & 0x3F) | ((value & 0x3) << 6)); } }
        public int Move2_PPUps { get { return (Data[0x21] & 0xC0) >> 6; } set { Data[0x21] = (byte)((Data[0x1E] & 0x3F) | ((value & 0x3) << 6)); } }
        public int Move3_PPUps { get { return (Data[0x22] & 0xC0) >> 6; } set { Data[0x22] = (byte)((Data[0x1F] & 0x3F) | ((value & 0x3) << 6)); } }
        public int Move4_PPUps { get { return (Data[0x23] & 0xC0) >> 6; } set { Data[0x23] = (byte)((Data[0x20] & 0x3F) | ((value & 0x3) << 6)); } }

        public int Stat_Level
        {
            get { return Data[0x24]; }
            set { Data[0x24] = (byte)value; Data[0x4] = (byte)value; }
        }
        public int Stat_HPMax { get { return BigEndian.ToUInt16(Data, 0x26); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x26); } }
        public int Stat_ATK { get { return BigEndian.ToUInt16(Data, 0x28); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x26); } }
        public int Stat_DEF { get { return BigEndian.ToUInt16(Data, 0x2A); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x28); } }
        public int Stat_SPE { get { return BigEndian.ToUInt16(Data, 0x2C); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x2A); } }
        public int Stat_SPC { get { return BigEndian.ToUInt16(Data, 0x2E); } set { BigEndian.GetBytes((ushort)value).CopyTo(Data, 0x2C); } }
        // Leave SPA and SPD as alias for SPC
        public int Stat_SPA { get { return Stat_SPC; } set { Stat_SPC = value; } }
        public int Stat_SPD { get { return Stat_SPC; } set { } }
        
        public string raw_Nick()
        {
        	return BitConverter.ToString(Data, 0x30, 0xF);
        }
        public string raw_OT()
        {
        	return BitConverter.ToString(Data, 0x40, 0xF);
        }
        
        public byte[] gb_string(int name) //0 nickname, 1 OT
        {
        	int offset = 0x30;
        	if (name == 1)
        		offset = 0x40;
        	byte[] jnick = new byte[6];
        	int i, j;
        	UInt16 temp = 0;
        	UInt16 temp2 = 0;
        	for (i=0;i<0x5;i++)
        	{
        		temp = BitConverter.ToUInt16(Data, offset+(i*2));
        		temp = (ushort)((ushort)((temp & 0xff) << 8) | ((temp >> 8) & 0xff));
        		
        		temp2 = 0;
        		
        		//Handle special ゜ (0xA59C) and ゛ (0xA59B) characters
        			//I don't actually know if the n64 game handles this characters as a separate character,
        			//but since it seems to follow some kind of standard and not use a custom font/encoding like the GB games,
        			//this seems the most reasonable way to do it (without actual knowledge of the specific encoding the game uses)
        		if(i<0x5)
        		{
        			temp2 = BitConverter.ToUInt16(Data, offset+(i*2)+2);
        			temp2 = (ushort)((ushort)((temp2 & 0xff) << 8) | ((temp2 >> 8) & 0xff));
        		}
        		
        		if (temp2 == 0xA59C)
        		{
        			if (temp == 0xA5A4)
        				jnick[i] = 0x01;
        			else if (temp == 0xA5A8)
        				jnick[i] = 0x03;
        			else if (temp == 0xA5AA)
        				jnick[i] = 0x04;
        			else if (temp == 0xA5CA)
        				jnick[i] = 0x14;
        			else if (temp == 0xA5CB)
        				jnick[i] = 0x14;
        			else if (temp == 0xA5CC)
        				jnick[i] = 0x16;
        			else if (temp == 0xA5CD)
        				jnick[i] = 0x17;
        			else if (temp == 0xA5CE)
        				jnick[i] = 0x18;
        			else if (temp == 0xA5DE)
        				jnick[i] = 0x1D;
        			else if (temp == 0xA5DF)
        				jnick[i] = 0x1E;
        			else if (temp == 0xA5E0)
        				jnick[i] = 0x1F;
        			else if (temp == 0xA5A3)
        				jnick[i] = 0x20;
        			else if (temp == 0xA542)
        				jnick[i] = 0x21;
        			else if (temp == 0xA544)
        				jnick[i] = 0x22;
        			else if (temp == 0xA548)
        				jnick[i] = 0x24;
        			else if (temp == 0xA54A)
        				jnick[i] = 0x25;
        			else if (temp == 0xA56A)
        				jnick[i] = 0x35;
        			else if (temp == 0xA56B)
        				jnick[i] = 0x36;
        			else if (temp == 0xA56C)
        				jnick[i] = 0x37;
        			else if (temp == 0xA56D)
        				jnick[i] = 0x38;
        			else if (temp == 0xA56E)
        				jnick[i] = 0x39;
        			else if (temp == 0xA57E)
        				jnick[i] = 0x3F;
        			else //This should not happen afaik
        			{
        				MessageBox.Show("Unrecognized n64 string encoding 0x"+temp.ToString("X")+" 0x"+temp2.ToString("X"));
        				jnick[i] = 0x00;
        			}
        			i++;//Skip the special character
        		}
        		else if (temp2 == 0xA59B)
        		{
        			if (temp == 0xA57E)
        				jnick[i] = 0x49;        			
        			else if (temp == 0xA582)
        				jnick[i] = 0x4D;
        			else //This should not happen afaik
        			{
        				MessageBox.Show("Unrecognized n64 string encoding 0x"+temp.ToString("X")+" 0x"+temp2.ToString("X"));
        				jnick[i] = 0x00;
        			}
        			i++;//Skip the special character
        		}
        		else //Not a special ゜ (0xA59C) or ゛ (0xA59B)
        		{
        		
	        		if (temp == 0)
	        		{
	        			jnick[i] = 0x50;
	        			break;
	        		}
	        		for (j=0xFF;j>0;j--) //Reverse for loop, because this way characters without special ゜ (0xA59C) or ゛ (0xA59B) are asigned first. 
	        		{
	        			if (temp == 0xA1A1)//Invalid chars are mapped to this
	        			{
	        				
	        			}
	        			else if (chartable[j] == temp)
	        			{
	        				jnick[i] = (byte)j;
	        				break;
	        			}
	        		}
        		}
        	}
        	return jnick;
        }

        public byte[] convertojpk1()
        {
        	byte [] temparray = new byte[44];
        	jpk1 jpoke = new jpk1(temparray);
        	

        	jpoke.Species = speciestable[Species];
        	jpoke.Stat_HPCurrent = Stat_HPCurrent;
        	jpoke.Stat_Level = Stat_Level;
        	jpoke.Status_Condition = Status_Condition;
        	jpoke.Type_A = Type_A;
        	jpoke.Type_B = Type_B;
        	jpoke.Catch_Rate = Catch_Rate;
        	jpoke.Move1 = Move1;
        	jpoke.Move2 = Move2;
        	jpoke.Move3 = Move3;
        	jpoke.Move4 = Move4;
        	jpoke.TID = TID;
        	jpoke.EXP = EXP;
        	jpoke.EV_HP = EV_HP;
        	jpoke.EV_ATK = EV_ATK;
        	jpoke.EV_DEF = EV_DEF;
        	jpoke.EV_SPE = EV_SPE;
        	jpoke.EV_SPC = EV_SPC;
        	jpoke.DV16 = DV16;
        	jpoke.Move1_PPUps = Move1_PPUps;
        	jpoke.Move2_PPUps = Move2_PPUps;
        	jpoke.Move3_PPUps = Move3_PPUps;
        	jpoke.Move4_PPUps = Move4_PPUps;
        	jpoke.Stat_HPMax = Stat_HPMax;
        	jpoke.Stat_ATK = Stat_ATK;
        	jpoke.Stat_DEF = Stat_DEF;
        	jpoke.Stat_SPE = Stat_SPE;
        	jpoke.Stat_SPC = Stat_SPC;
        	
        	byte[] finalpoke = new byte[59];
        	
        	finalpoke[0] = 0x01;
        	finalpoke[1] = (byte)Species;
        	finalpoke[2] = 0xFF;
        	jpoke.Data.CopyTo(finalpoke, 3);
        	
        	gb_string(1).CopyTo(finalpoke, 47);//OT
        	gb_string(0).CopyTo(finalpoke, 53);//Nickname
        	
        	//To do: NICK and OT conversion
        	
        	return finalpoke;
        	
        }
        internal int[] speciestable = new int[152];
        internal void init_speciestable()
        {
			speciestable[1] = 0x99;
			speciestable[2] = 0x9;
			speciestable[3] = 0x9A;
			speciestable[4] = 0xB0;
			speciestable[5] = 0xB2;
			speciestable[6] = 0xB4;
			speciestable[7] = 0xB1;
			speciestable[8] = 0xB3;
			speciestable[9] = 0x1C;
			speciestable[10] = 0x7B;
			speciestable[11] = 0x7C;
			speciestable[12] = 0x7D;
			speciestable[13] = 0x70;
			speciestable[14] = 0x71;
			speciestable[15] = 0x72;
			speciestable[16] = 0x24;
			speciestable[17] = 0x96;
			speciestable[18] = 0x97;
			speciestable[19] = 0xA5;
			speciestable[20] = 0xA6;
			speciestable[21] = 0x5;
			speciestable[22] = 0x23;
			speciestable[23] = 0x6C;
			speciestable[24] = 0x2D;
			speciestable[25] = 0x54;
			speciestable[26] = 0x55;
			speciestable[27] = 0x60;
			speciestable[28] = 0x61;
			speciestable[29] = 0x0F;
			speciestable[30] = 0xA8;
			speciestable[31] = 0x10;
			speciestable[32] = 0x3;
			speciestable[33] = 0xA7;
			speciestable[34] = 0x7;
			speciestable[35] = 0x4;
			speciestable[36] = 0x8E;
			speciestable[37] = 0x52;
			speciestable[38] = 0x53;
			speciestable[39] = 0x64;
			speciestable[40] = 0x65;
			speciestable[41] = 0x6B;
			speciestable[42] = 0x82;
			speciestable[43] = 0xB9;
			speciestable[44] = 0xBA;
			speciestable[45] = 0xBB;
			speciestable[46] = 0x6D;
			speciestable[47] = 0x2E;
			speciestable[48] = 0x41;
			speciestable[49] = 0x77;
			speciestable[50] = 0x3B;
			speciestable[51] = 0x76;
			speciestable[52] = 0x4D;
			speciestable[53] = 0x90;
			speciestable[54] = 0x2F;
			speciestable[55] = 0x80;
			speciestable[56] = 0x39;
			speciestable[57] = 0x75;
			speciestable[58] = 0x21;
			speciestable[59] = 0x14;
			speciestable[60] = 0x47;
			speciestable[61] = 0x6E;
			speciestable[62] = 0x6F;
			speciestable[63] = 0x94;
			speciestable[64] = 0x26;
			speciestable[65] = 0x95;
			speciestable[66] = 0x6A;
			speciestable[67] = 0x29;
			speciestable[68] = 0x7E;
			speciestable[69] = 0xBC;
			speciestable[70] = 0xBD;
			speciestable[71] = 0xBE;
			speciestable[72] = 0x18;
			speciestable[73] = 0x9B;
			speciestable[74] = 0xA9;
			speciestable[75] = 0x27;
			speciestable[76] = 0x31;
			speciestable[77] = 0xA3;
			speciestable[78] = 0xA4;
			speciestable[79] = 0x25;
			speciestable[80] = 0x8;
			speciestable[81] = 0xAD;
			speciestable[82] = 0x36;
			speciestable[83] = 0x40;
			speciestable[84] = 0x46;
			speciestable[85] = 0x74;
			speciestable[86] = 0x3A;
			speciestable[87] = 0x78;
			speciestable[88] = 0x0D;
			speciestable[89] = 0x88;
			speciestable[90] = 0x17;
			speciestable[91] = 0x8B;
			speciestable[92] = 0x19;
			speciestable[93] = 0x93;
			speciestable[94] = 0x0E;
			speciestable[95] = 0x22;
			speciestable[96] = 0x30;
			speciestable[97] = 0x81;
			speciestable[98] = 0x4E;
			speciestable[99] = 0x8A;
			speciestable[100] = 0x6;
			speciestable[101] = 0x8D;
			speciestable[102] = 0x0C;
			speciestable[103] = 0x0A;
			speciestable[104] = 0x11;
			speciestable[105] = 0x91;
			speciestable[106] = 0x2B;
			speciestable[107] = 0x2C;
			speciestable[108] = 0x0B;
			speciestable[109] = 0x37;
			speciestable[110] = 0x8F;
			speciestable[111] = 0x12;
			speciestable[112] = 0x1;
			speciestable[113] = 0x28;
			speciestable[114] = 0x1E;
			speciestable[115] = 0x2;
			speciestable[116] = 0x5C;
			speciestable[117] = 0x5D;
			speciestable[118] = 0x9D;
			speciestable[119] = 0x9E;
			speciestable[120] = 0x1B;
			speciestable[121] = 0x98;
			speciestable[122] = 0x2A;
			speciestable[123] = 0x1A;
			speciestable[124] = 0x48;
			speciestable[125] = 0x35;
			speciestable[126] = 0x33;
			speciestable[127] = 0x1D;
			speciestable[128] = 0x3C;
			speciestable[129] = 0x85;
			speciestable[130] = 0x16;
			speciestable[131] = 0x13;
			speciestable[132] = 0x4C;
			speciestable[133] = 0x66;
			speciestable[134] = 0x69;
			speciestable[135] = 0x68;
			speciestable[136] = 0x67;
			speciestable[137] = 0xAA;
			speciestable[138] = 0x62;
			speciestable[139] = 0x63;
			speciestable[140] = 0x5A;
			speciestable[141] = 0x5B;
			speciestable[142] = 0xAB;
			speciestable[143] = 0x84;
			speciestable[144] = 0x4A;
			speciestable[145] = 0x4B;
			speciestable[146] = 0x49;
			speciestable[147] = 0x58;
			speciestable[148] = 0x59;
			speciestable[149] = 0x42;
			speciestable[150] = 0x83;
			speciestable[151] = 0x15;
			/*
			//Some Gen 1 Missingnos
			speciestable[] = 0x1F;
			speciestable[] = 0x20;
			speciestable[] = 0x32;
			speciestable[] = 0x34;
			speciestable[] = 0x38;
			speciestable[] = 0x3D;
			speciestable[] = 0x3E;
			speciestable[] = 0x3F;
			speciestable[] = 0x43;
			speciestable[] = 0x44;
			speciestable[] = 0x45;
			speciestable[] = 0x4F;
			speciestable[] = 0x50;
			speciestable[] = 0x51;
			speciestable[] = 0x56;
			speciestable[] = 0x57;
			speciestable[] = 0x5E;
			speciestable[] = 0x5F;
			speciestable[] = 0x73;
			speciestable[] = 0x79;
			speciestable[] = 0x7A;
			speciestable[] = 0x7F;
			speciestable[] = 0x86;
			speciestable[] = 0x87;
			speciestable[] = 0x89;
			speciestable[] = 0x8C;
			speciestable[] = 0x92;
			speciestable[] = 0x9C;
			speciestable[] = 0x9F;
			speciestable[] = 0xA0;
			speciestable[] = 0xA1;
			speciestable[] = 0xA2;
			speciestable[] = 0xAC;
			speciestable[] = 0xAE;
			speciestable[] = 0xAF;
			speciestable[] = 0xB5;
			speciestable[] = 0xB6;
			speciestable[] = 0xB7;
			speciestable[] = 0xB8;
			*/
        	/*
			speciestable[112] = 0x1;
			speciestable[115] = 0x2;
			speciestable[32] = 0x3;
			speciestable[35] = 0x4;
			speciestable[21] = 0x5;
			speciestable[100] = 0x6;
			speciestable[34] = 0x7;
			speciestable[80] = 0x8;
			speciestable[2] = 0x9;
			speciestable[103] = 0x0A;
			speciestable[108] = 0x0B;
			speciestable[102] = 0x0C;
			speciestable[88] = 0x0D;
			speciestable[94] = 0x0E;
			speciestable[29] = 0x0F;
			speciestable[31] = 0x10;
			speciestable[104] = 0x11;
			speciestable[111] = 0x12;
			speciestable[131] = 0x13;
			speciestable[59] = 0x14;
			speciestable[151] = 0x15;
			speciestable[130] = 0x16;
			speciestable[90] = 0x17;
			speciestable[72] = 0x18;
			speciestable[92] = 0x19;
			speciestable[123] = 0x1A;
			speciestable[120] = 0x1B;
			speciestable[9] = 0x1C;
			speciestable[127] = 0x1D;
			speciestable[114] = 0x1E;
			speciestable[58] = 0x21;
			speciestable[95] = 0x22;
			speciestable[22] = 0x23;
			speciestable[16] = 0x24;
			speciestable[79] = 0x25;
			speciestable[64] = 0x26;
			speciestable[75] = 0x27;
			speciestable[113] = 0x28;
			speciestable[67] = 0x29;
			speciestable[122] = 0x2A;
			speciestable[106] = 0x2B;
			speciestable[107] = 0x2C;
			speciestable[24] = 0x2D;
			speciestable[47] = 0x2E;
			speciestable[54] = 0x2F;
			speciestable[96] = 0x30;
			speciestable[76] = 0x31;
			speciestable[126] = 0x33;
			speciestable[125] = 0x35;
			speciestable[82] = 0x36;
			speciestable[109] = 0x37;
			speciestable[56] = 0x39;
			speciestable[86] = 0x3A;
			speciestable[50] = 0x3B;
			speciestable[128] = 0x3C;
			speciestable[83] = 0x40;
			speciestable[48] = 0x41;
			speciestable[149] = 0x42;
			speciestable[84] = 0x46;
			speciestable[60] = 0x47;
			speciestable[124] = 0x48;
			speciestable[146] = 0x49;
			speciestable[144] = 0x4A;
			speciestable[145] = 0x4B;
			speciestable[132] = 0x4C;
			speciestable[52] = 0x4D;
			speciestable[98] = 0x4E;
			speciestable[37] = 0x52;
			speciestable[38] = 0x53;
			speciestable[25] = 0x54;
			speciestable[26] = 0x55;
			speciestable[147] = 0x58;
			speciestable[148] = 0x59;
			speciestable[140] = 0x5A;
			speciestable[141] = 0x5B;
			speciestable[116] = 0x5C;
			speciestable[117] = 0x5D;
			speciestable[27] = 0x60;
			speciestable[28] = 0x61;
			speciestable[138] = 0x62;
			speciestable[139] = 0x63;
			speciestable[39] = 0x64;
			speciestable[40] = 0x65;
			speciestable[133] = 0x66;
			speciestable[136] = 0x67;
			speciestable[135] = 0x68;
			speciestable[134] = 0x69;
			speciestable[66] = 0x6A;
			speciestable[41] = 0x6B;
			speciestable[23] = 0x6C;
			speciestable[46] = 0x6D;
			speciestable[61] = 0x6E;
			speciestable[62] = 0x6F;
			speciestable[13] = 0x70;
			speciestable[14] = 0x71;
			speciestable[15] = 0x72;
			speciestable[85] = 0x74;
			speciestable[57] = 0x75;
			speciestable[51] = 0x76;
			speciestable[49] = 0x77;
			speciestable[87] = 0x78;
			speciestable[10] = 0x7B;
			speciestable[11] = 0x7C;
			speciestable[12] = 0x7D;
			speciestable[68] = 0x7E;
			speciestable[55] = 0x80;
			speciestable[97] = 0x81;
			speciestable[42] = 0x82;
			speciestable[150] = 0x83;
			speciestable[143] = 0x84;
			speciestable[129] = 0x85;
			speciestable[89] = 0x88;
			speciestable[99] = 0x8A;
			speciestable[91] = 0x8B;
			speciestable[101] = 0x8D;
			speciestable[36] = 0x8E;
			speciestable[110] = 0x8F;
			speciestable[53] = 0x90;
			speciestable[105] = 0x91;
			speciestable[93] = 0x93;
			speciestable[63] = 0x94;
			speciestable[65] = 0x95;
			speciestable[17] = 0x96;
			speciestable[18] = 0x97;
			speciestable[121] = 0x98;
			speciestable[1] = 0x99;
			speciestable[3] = 0x9A;
			speciestable[73] = 0x9B;
			speciestable[118] = 0x9D;
			speciestable[119] = 0x9E;
			speciestable[77] = 0xA3;
			speciestable[78] = 0xA4;
			speciestable[19] = 0xA5;
			speciestable[20] = 0xA6;
			speciestable[33] = 0xA7;
			speciestable[30] = 0xA8;
			speciestable[74] = 0xA9;
			speciestable[137] = 0xAA;
			speciestable[142] = 0xAB;
			speciestable[81] = 0xAD;
			speciestable[4] = 0xB0;
			speciestable[7] = 0xB1;
			speciestable[5] = 0xB2;
			speciestable[8] = 0xB3;
			speciestable[6] = 0xB4;
			speciestable[43] = 0xB9;
			speciestable[44] = 0xBA;
			speciestable[45] = 0xBB;
			speciestable[69] = 0xBC;
			speciestable[70] = 0xBD;
			speciestable[71] = 0xBE;
			*/
        }
        internal UInt16[] chartable = new UInt16[256];
        internal void init_table()
        {
        	//All commented chars become 0xA1A1 in stadium (space character)
        	chartable[0x00] = 0x00; //NULL
        	//chartable[0x01] = 0xA5A4; //イ゛ //SPECIAL (2 CHARACTERS) A5A4 + A59B
        	//chartable[0x02] = 0xA5F4; //ヴ
        	//chartable[0x03] = 0xA5A8; //エ゛ //SPECIAL (2 CHARACTERS) A5A8 + A59B
        	//chartable[0x04] = 0xA5AA; //オ゛ //SPECIAL (2 CHARACTERS) A5AA + A59B
        	chartable[0x05] = 0xA5AC; //ガ
        	chartable[0x06] = 0xA5AE; //ギ
			chartable[0x07] = 0xA5B0; //グ
			chartable[0x08] = 0xA5B2; //ゲ
			chartable[0x09] = 0xA5B4; //ゴ
			chartable[0x0A] = 0xA5B6; //ザ
			chartable[0x0B] = 0xA5B8; //ジ
			chartable[0x0C] = 0xA5BA; //ズ
			chartable[0x0D] = 0xA5BC; //ゼ
			chartable[0x0E] = 0xA5BE; //ゾ
			chartable[0x0F] = 0xA5C0; //ダ
			
			chartable[0x10] = 0xA5C2; //ヂ
			chartable[0x11] = 0xA5C5; //ヅ
			chartable[0x12] = 0xA5C7; //デ
			chartable[0x13] = 0xA5C9; //ド
			//chartable[0x14] = 0xA5CA; //ナ゛ //SPECIAL (2 CHARACTERS) A5CA + A59B
			//chartable[0x15] = 0xA5CB; //ニ゛ //SPECIAL (2 CHARACTERS) A5CB + A59B
			//chartable[0x16] = 0xA5CC; //ヌ゛ //SPECIAL (2 CHARACTERS) A5CC + A59B
			//chartable[0x17] = 0xA5CD; //ネ゛ //SPECIAL (2 CHARACTERS) A5CD + A59B
			//chartable[0x18] = 0xA5CE; //ノ゛ //SPECIAL (2 CHARACTERS) A5CE + A59B
			chartable[0x19] = 0xA5D0; //バ
			chartable[0x1A] = 0xA5D3; //ビ
			chartable[0x1B] = 0xA5D6; //ブ
			chartable[0x1C] = 0xA5DC; //ボ
			//chartable[0x1D] = 0xA5DE; //マ゛ //SPECIAL (2 CHARACTERS) A5DE + A59B
			//chartable[0x1E] = 0xA5DF; //ミ゛ //SPECIAL (2 CHARACTERS) A5DF + A59B
			//chartable[0x1F] = 0xA5E0; //ム゛ //SPECIAL (2 CHARACTERS) A5E0 + A59B
			
			//chartable[0x20] = 0xA5A3; //ィ゛ //SPECIAL (2 CHARACTERS) A5A3 + A59B
			//chartable[0x21] = 0xA542; //あ゛ //SPECIAL (2 CHARACTERS) A543 + A59B
			//chartable[0x22] = 0xA544; //い゛ //SPECIAL (2 CHARACTERS) A544 + A59B
			//chartable[0x23] = 0xA5F4; //ゔ
			//chartable[0x24] = 0xA548; //え゛ //SPECIAL (2 CHARACTERS) A548 + A59B
			//chartable[0x25] = 0xA54A; //お゛ //SPECIAL (2 CHARACTERS) A54A + A59B
			chartable[0x26] = 0xA4AC; //が
			chartable[0x27] = 0xA4AE; //ぎ
			chartable[0x28] = 0xA4B0; //ぐ
			chartable[0x29] = 0xA4B2; //げ
			chartable[0x2A] = 0xA4B4; //ご
			chartable[0x2B] = 0xA4B6; //ざ
			chartable[0x2C] = 0xA4B8; //じ
			chartable[0x2D] = 0xA4BA; //ず
			chartable[0x2E] = 0xA4BC; //ぜ
			chartable[0x2F] = 0xA4BE; //ぞ
			
			chartable[0x30] = 0xA4C0; //だ
			chartable[0x31] = 0xA4C2; //ぢ
			chartable[0x32] = 0xA4C5; //づ
			chartable[0x33] = 0xA4C7; //で
			chartable[0x34] = 0xA4C9; //ど
			//chartable[0x35] = 0xA56A; //な゛ //SPECIAL (2 CHARACTERS) A56A + A59B
			//chartable[0x36] = 0xA56B; //に゛ //SPECIAL (2 CHARACTERS) A56B + A59B
			//chartable[0x37] = 0xA56C; //ぬ゛ //SPECIAL (2 CHARACTERS) A56C + A59B
			//chartable[0x38] = 0xA56D; //ね゛ //SPECIAL (2 CHARACTERS) A56D + A59B
			//chartable[0x39] = 0xA56E; //の゛ //SPECIAL (2 CHARACTERS) A56E + A59B
			chartable[0x3A] = 0xA4D0; //ば
			chartable[0x3B] = 0xA4D3; //び
			chartable[0x3C] = 0xA4D6; //ぶ
			chartable[0x3D] = 0xA4DA; //べ
			chartable[0x3E] = 0xA4DD; //ぼ
			//chartable[0x3F] = 0xA57E; //ま゛ //SPECIAL (2 CHARACTERS) A57E + A59B
			
			//chartable[0x40] = 0xA5D1; //パ
			//chartable[0x41] = 0xA5D4; //ピ
			chartable[0x42] = 0xA5D7; //プ
			chartable[0x43] = 0xA5DD; //ポ
			chartable[0x44] = 0xA4D1; //ぱ
			chartable[0x45] = 0xA4D4; //ぴ
			chartable[0x46] = 0xA4D7; //ぷ
			//chartable[0x47] = 0xAA; //ぺ
			//chartable[0x48] = 0xA57D; //ぽ
			//chartable[0x49] = 0xA57E; //ま゜ //SPECIAL (2 CHARACTERS) A57E + A59C
			//chartable[0x4A] = 0x00; //Control
			//chartable[0x4B] = 0x00; //Control
			//chartable[0x4C] = 0x00; //Control
			//chartable[0x4D] = 0xA582; //も゜ //SPECIAL (2 CHARACTERS) A582 + A59C
			//chartable[0x4E] = 0x00; //Control
			//chartable[0x4F] = 0x00; //Control
			
			/*
			chartable[0x50] = 0x00; //Control
			chartable[0x51] = 0x00; //Control
			chartable[0x52] = 0x00; //Control
			chartable[0x53] = 0x00; //Control
			chartable[0x54] = 0x00; //Control
			chartable[0x55] = 0x00; //Control
			chartable[0x56] = 0x00; //Control
			chartable[0x57] = 0x00; //Control
			chartable[0x58] = 0x00; //Control
			chartable[0x59] = 0x00; //Control
			chartable[0x5A] = 0x00; //Control
			chartable[0x5B] = 0x00; //Control
			chartable[0x5C] = 0x00; //Control
			chartable[0x5D] = 0x00; //Control
			chartable[0x5E] = 0x00; //Control
			chartable[0x5F] = 0x00; //Control
			*/
			
			chartable[0x60] = 0xA3C1; //A
			chartable[0x61] = 0xA3C2; //B
			chartable[0x62] = 0xA3C3; //C
			chartable[0x63] = 0xA3C4; //D
			chartable[0x64] = 0xA3C5; //E
			chartable[0x65] = 0xA3C6; //F
			chartable[0x66] = 0xA3C7; //G
			chartable[0x67] = 0xA3C8; //H
			chartable[0x68] = 0xA3C9; //I
			chartable[0x69] = 0xA3CA; //V
			chartable[0x6A] = 0xA3CB; //S
			chartable[0x6B] = 0xA3CC; //L
			chartable[0x6C] = 0xA3CD; //M
			chartable[0x6D] = 0xA5CE; //: 					//N in stadium 1 jap
			chartable[0x6E] = 0xA4A3; //ぃ
			chartable[0x6F] = 0xA4A5; //ぅ
			
			chartable[0x70] = 0xA3D1; //「 					//Q in stadium 1 jap
			chartable[0x71] = 0xA3D2; //」 					//R in stadium 1 jap
			chartable[0x72] = 0xA3D3; //『 					//S in stadium 1 jap
			chartable[0x73] = 0xA3D4; //』 					//T in stadium 1 jap
			chartable[0x74] = 0xA3D5; //・ 					//U in stadium 1 jap
			chartable[0x75] = 0xA3D6; //… 					//V in stadium 1 jap
			chartable[0x76] = 0xA4A1; //ぁ
			chartable[0x77] = 0xA4A7; //ぇ
			chartable[0x78] = 0xA4A9; //ぉ
			chartable[0x79] = 0xA3DA; //Unknown unicode 	//Z in stadium 1 jap
			//chartable[0x7A] = 0xA53D; //=
			//chartable[0x7B] = 0; //Unknown unicode
			//chartable[0x7C] = 0xA57C; //| ****The character in GB is actually two || (need to check if it is a valid in-game text input)
			//chartable[0x7D] = 0; //Unknown unicode
			//chartable[0x7E] = 0; //Unknown unicode
			chartable[0x7F] = 0xA1A1; // space character
			
			chartable[0x80] = 0xA5A2; //ア
			chartable[0x81] = 0xA5A4; //イ
			chartable[0x82] = 0xA5A6; //ウ
			chartable[0x83] = 0xA5A8; //エ
			chartable[0x84] = 0xA5AA; //オ
			chartable[0x85] = 0xA5AB; //カ 
			chartable[0x86] = 0xA5AD; //キ
			chartable[0x87] = 0xA5AF; //ク
			chartable[0x88] = 0xA5B1; //ケ
			chartable[0x89] = 0xA5B3; //コ
			chartable[0x8A] = 0xA5B5; //サ
			chartable[0x8B] = 0xA5B7; //シ
			chartable[0x8C] = 0xA5B9; //ス
			chartable[0x8D] = 0xA5BB; //セ
			chartable[0x8E] = 0xA5BD; //ソ
			chartable[0x8E] = 0xA5BF; //タ
			
			
			chartable[0x90] = 0xA5C1; //チ
			chartable[0x91] = 0xA5C4; //ツ
			chartable[0x92] = 0xA5C6; //テ
			chartable[0x93] = 0xA5C8; //ト
			chartable[0x94] = 0xA5CA; //ナ
			chartable[0x95] = 0xA5CB; //ニ
			chartable[0x96] = 0xA5CC; //ヌ
			chartable[0x97] = 0xA5CD; //ネ
			chartable[0x98] = 0xA5CE; //ノ
			chartable[0x99] = 0xA5CF; //ハ
			chartable[0x9A] = 0xA5D2; //ヒ
			chartable[0x9B] = 0xA5D5; //フ
			chartable[0x9C] = 0xA5DB; //ホ
			chartable[0x9D] = 0xA5DE; //マ
			chartable[0x9E] = 0xA5DF; //ミ
			chartable[0x9F] = 0xA5E0; //ム
			
			chartable[0xA0] = 0xA5E1; //メ
			chartable[0xA1] = 0xA5E2; //モ
			chartable[0xA2] = 0xA5E4; //ヤ
			chartable[0xA3] = 0xA5E6; //ユ
			chartable[0xA4] = 0xA5E8; //ヨ
			chartable[0xA5] = 0xA5E9; //ラ
			chartable[0xA6] = 0xA5EB; //ル
			chartable[0xA7] = 0xA5EC; //レ
			chartable[0xA8] = 0xA5ED; //ロ
			chartable[0xA9] = 0xA5EF; //ワ
			chartable[0xAA] = 0xA5F2; //ヲ
			chartable[0xAB] = 0xA5F3; //ン
			chartable[0xAC] = 0xA5C3; //ッ
			chartable[0xAD] = 0xA5E3; //ャ
			chartable[0xAE] = 0xA5E5; //ュ
			chartable[0xAF] = 0xA5E7; //ョ
			
			
			chartable[0xB0] = 0xA5A3; //ィ
			chartable[0xB1] = 0xA4A2; //あ
			chartable[0xB2] = 0xA4A4; //い
			chartable[0xB3] = 0xA4A6; //う
			chartable[0xB4] = 0xA4A8; //え
			chartable[0xB5] = 0xA4AA; //お
			chartable[0xB6] = 0xA4AB; //か
			chartable[0xB7] = 0xA4AD; //き
			chartable[0xB8] = 0xA4AF; //く
			chartable[0xB9] = 0xA4B1; //け
			chartable[0xBA] = 0xA4B3; //さ
			chartable[0xBB] = 0xA4B5; //こ
			chartable[0xBC] = 0xA4B7; //し
			chartable[0xBD] = 0xA4B9; //す
			chartable[0xBE] = 0xA4BB; //せ
			chartable[0xBF] = 0xA4BD; //そ
			
			chartable[0xc0] = 0xA4BF; //た
			chartable[0xc1] = 0xA4C1; //ち
			chartable[0xc2] = 0xA4C4; //つ
			chartable[0xc3] = 0xA4C6; //て
			chartable[0xc4] = 0xA4C8; //と
			chartable[0xc5] = 0xA4CA; //な
			chartable[0xc6] = 0xA4CB; //に
			chartable[0xc7] = 0xA4CC; //ぬ
			chartable[0xc8] = 0xA4CD; //ね
			chartable[0xc9] = 0xA4CE; //の
			chartable[0xcA] = 0xA4CF; //は
			chartable[0xcB] = 0xA4D2; //ひ
			chartable[0xcC] = 0xA4D5; //ふ
			chartable[0xcD] = 0xA4D8; //へ
			chartable[0xcE] = 0xA4DB; //ほ
			chartable[0xcF] = 0xA4DE; //ま
			
			chartable[0xD0] = 0xA4DF; //み
			chartable[0xD1] = 0xA4E0; //む
			chartable[0xD2] = 0xA4E1; //め
			chartable[0xD3] = 0xA4E2; //も
			chartable[0xD4] = 0xA4E4; //や
			chartable[0xD5] = 0xA4E6; //ゆ
			chartable[0xD6] = 0xA4E8; //よ
			chartable[0xD7] = 0x44E9; //ら
			chartable[0xD8] = 0xA4EA; //り
			chartable[0xD9] = 0xA4EB; //る
			chartable[0xDA] = 0xA4EC; //れ
			chartable[0xDB] = 0xA4ED; //ろ
			chartable[0xDC] = 0xA4EF; //わ //small version isn't in GB charset (A58E)
			chartable[0xDD] = 0xA4F2; //を
			chartable[0xDE] = 0xA4F3; //ん
			chartable[0xDF] = 0xA4C3; //っ
			
			chartable[0xE0] = 0xA4E3; //ゃ
			chartable[0xE1] = 0xA4E5; //ゅ
			chartable[0xE2] = 0xA4E7; //ょ
			chartable[0xE3] = 0xA1BC; //ー
			chartable[0xE4] = 0xA1A3; //゜ //needs special handling?
			chartable[0xE5] = 0xA1A5; //゛ //needs special handling?
			chartable[0xE6] = 0xA1A9; //?
			chartable[0xE7] = 0xA1AA; //!
			chartable[0xE8] = 0xA1A; //。					//some sort of ' punctuation, but appers in the bottom in stadium 1 jap
			chartable[0xE9] = 0xA5A1; //ァ
			chartable[0xEA] = 0xA5A5; //ゥ
			chartable[0xEB] = 0xA5A7; //ェ
			//chartable[0xEC] = 0xA5B7; //▷
			//chartable[0xED] = 0xA5B6; //▶
			//chartable[0xEE] = 0xA5BC; //▼
			chartable[0xEF] = 0xA1E9; //♂
			
			//chartable[0xF0] = 0xA5A5; //円 //Yen sign, conflicts with ゥ (0xEA in GB table) (need to check if it is a valid in-game text input)
			//chartable[0xF1] = 0xA5D7; //× //Conflicts with プ (0x42 in GB table) (need to check if it is a valid in-game text input)
			//chartable[0xF2] = 0xA52E; //.
			//chartable[0xF3] = 0xA52F; //'/'
			//chartable[0xF4] = 0xA5A9; //ォ
			chartable[0xF5] = 0xA1EA; //♀
			chartable[0xF6] = 0xA3B0; //0
			chartable[0xF7] = 0xA3B1; //1
			chartable[0xF8] = 0xA3B2; //2
			chartable[0xF9] = 0xA3B3; //3
			chartable[0xFA] = 0xA3B4; //4
			chartable[0xFB] = 0xA3B5; //5
			chartable[0xFC] = 0xA3B6; //6
			chartable[0xFD] = 0xA3B7; //7
			chartable[0xFE] = 0xA3B8; //8
			chartable[0xFF] = 0xA3B9; //9
        }

	    
	}
}
