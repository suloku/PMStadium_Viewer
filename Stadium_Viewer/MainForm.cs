/*
 * Created by SharpDevelop.
 * User: sergi
 * Date: 07/02/2017
 * Time: 19:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Stadium_Viewer
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			PartySlot.Value = 0;
			offset_val.Maximum = 0xFFFFFFFF;
			trainer_start = fifties_tournament;
							trainers.SelectedIndex = 0;
				cups.SelectedIndex = 0;
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public string z64filter = "N64 rom|*.z64|All Files (*.*)|*.*";
		public string jpk1filter = "PkHex JPK1|*.jpk1|All Files (*.*)|*.*";
		public byte[] savebuffer;
		public static js1pk pokemon;
		
		public int trainer_start;
		public int fifties_tournament = 0x175980;
		public int pokecup = 0x177690;
		public int greatcup = 0x178610;
		public int ultracup = 0x179590;
		public int mastercup = 0x17A510;
		
		void Load_butClick(object sender, EventArgs e)
		{
			load_rom(null);
		}		
		void load_rom(string filepath)
		{
			string path = filepath;
			int filesize = FileIO.load_file(ref savebuffer, ref path, z64filter);

			if( filesize == 0x1000000 )
			{
				filelocation.Text = path;
				offset_val.Value = trainer_start;
				load_pokemon();
			}
			else
			{
				MessageBox.Show("Invalid file.");
			}

		}
		void load_pokemon()
		{
			if (filelocation.Text != "")
			{
				offset_val.Value = (int)(trainer_start+(trainers.SelectedIndex*js1pk.Size*6)+(trainers.SelectedIndex*0x10)+(PartySlot.Value*js1pk.Size));
				pokemon = new js1pk(getDatafromSave((int)offset_val.Value, js1pk.Size));
				
				//Fill data
				Species.SelectedIndex = pokemon.Species;
				ID.Value = pokemon.TID;
				move1.SelectedIndex = pokemon.Move1;
				move2.SelectedIndex = pokemon.Move2;
				move3.SelectedIndex = pokemon.Move3;
				move4.SelectedIndex = pokemon.Move4;
				
				ppup1.SelectedIndex = pokemon.Move1_PPUps;
				ppup2.SelectedIndex = pokemon.Move2_PPUps;
				ppup3.SelectedIndex = pokemon.Move3_PPUps;
				ppup4.SelectedIndex = pokemon.Move4_PPUps;
				
				
				hp.Value = pokemon.IV_HP;
				atk.Value = pokemon.IV_ATK;
				def.Value = pokemon.IV_DEF;
				spc.Value = pokemon.IV_SPC;
				spe.Value = pokemon.IV_SPE;
				
				hpEv.Value = pokemon.EV_HP;
				atkEv.Value = pokemon.EV_ATK;
				defEv.Value = pokemon.EV_DEF;
				spcEv.Value = pokemon.EV_SPC;
				speEv.Value = pokemon.EV_SPE;
				
				//Stats
				hpStat.Text = pokemon.Stat_HPCurrent.ToString()+"/"+pokemon.Stat_HPMax;
				atkStat.Text = pokemon.Stat_ATK.ToString();
				defStat.Text = pokemon.Stat_DEF.ToString();
				spcStat.Text = pokemon.Stat_SPC.ToString();
				speStat.Text = pokemon.Stat_SPE.ToString();
				
				level.Value = pokemon.Stat_Level;
				exp.Value = pokemon.EXP;
				
				//Extra data
				type1.SelectedIndex = pokemon.Type_A;
				type2.SelectedIndex = pokemon.Type_B;
				catchrate.Value = pokemon.Catch_Rate;
				status.Text = "0x"+pokemon.Status_Condition.ToString("X");
				
				//OT and nickname
				otraw.Text = pokemon.raw_OT();
				nickraw.Text = pokemon.raw_Nick();
			}
		}
		byte[] getDatafromSave(int Offset, int Length)
        {
            return savebuffer.Skip(Offset).Take(Length).ToArray();
        }

		void PartySlotValueChanged(object sender, EventArgs e)
		{
			load_pokemon();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void Offset_valValueChanged(object sender, EventArgs e)
		{
			//load_pokemon();
		}
		void Jpk1Click(object sender, EventArgs e)
		{
			FileIO.save_file(pokemon.convertojpk1(), jpk1filter);
		}
		void CupsSelectedIndexChanged(object sender, EventArgs e)
		{
			trainers.Items.Clear();
			trainers.Items.AddRange(new object[] {
							"0: Bug Catcher",
							"1: Burglar",
							"2: Bird Keeper",
							"3: Picnicker",
							"4: Super Nerd",
							"5: Youngster",
							"6: Camper",
							"7: Lass"});
			
			switch(cups.SelectedIndex)
			{
				case 0:
					trainer_start = fifties_tournament;
					trainers.Items.Clear();
					trainers.Items.AddRange(new object[] {
							"0: Katou Kazuhito",
							"1: Yukiyo Jiro",
							"2: Takahashi Jun",
							"3: Toru",
							"4: Suzuki Yusuke",
							"5: Fujita Mika",
							"6: Hiro Hayashiyasu",
							"7: Yamadataka Hisa",
							"8: Kitagawa Yasunori",
							"9: Nishimura Shun",
							"10: Imai Daisuke",
							"11: Yuwamasa Kenji",
							"12: Hiroki Yoshii",
							"13: Tomitaisei Yoshi",
							"14: Hiro Fuchiwakiaki"});
					break;
				case 1:
					trainer_start = pokecup;
					break;
				case 2:
					trainer_start = greatcup;
					break;
				case 3:
					trainer_start = ultracup;
					break;
				case 4:
					trainer_start = mastercup;
					break;
			}
			trainers.SelectedIndex = 0;
			PartySlot.Value = 0;
			load_pokemon();
		}
		void TrainersSelectedIndexChanged(object sender, EventArgs e)
		{
			PartySlot.Value = 0;
			load_pokemon();
		}
	}
}

/*
chartable[0x00] = 0x00 //NULL
chartable[0x01] = 0xA5A4 //イ゛ //SPECIAL (2 CHARACTERS) A5A4 + A59B
chartable[0x02] = 0xA5F4 //ヴ
chartable[0x03] = 0xA5A8 //エ゛ //SPECIAL (2 CHARACTERS) A5A8 + A59B
chartable[0x04] = 0xA5AA //オ゛ //SPECIAL (2 CHARACTERS) A5AA + A59B
chartable[0x05] = 0xA5AC //ガ
chartable[0x06] = 0xA5AE //ギ
chartable[0x07] = 0xA5B0 //グ
chartable[0x08] = 0xA5B2 //ゲ
chartable[0x09] = 0xA5B4 //ゴ
chartable[0x0A] = 0xA5B6 //ザ
chartable[0x0B] = 0xA5B8 //ジ
chartable[0x0C] = 0xA5BA //ズ
chartable[0x0D] = 0xA5BC //ゼ
chartable[0x0E] = 0xA5BE //ゾ
chartable[0x0F] = 0xA5C0 //ダ

chartable[0x10] = 0xA5C2 //ヂ
chartable[0x11] = 0xA5C5 //ヅ
chartable[0x12] = 0xA5C7 //デ
chartable[0x13] = 0xA5C9 //ド
chartable[0x14] = 0xA5CA //ナ゛ //SPECIAL (2 CHARACTERS) A5CA + A59B
chartable[0x15] = 0xA5CB //ニ゛ //SPECIAL (2 CHARACTERS) A5CB + A59B
chartable[0x16] = 0xA5CC //ヌ゛ //SPECIAL (2 CHARACTERS) A5CC + A59B
chartable[0x17] = 0xA5CD //ネ゛ //SPECIAL (2 CHARACTERS) A5CD + A59B
chartable[0x18] = 0xA5CE //ノ゛ //SPECIAL (2 CHARACTERS) A5CE + A59B
chartable[0x19] = 0xA5D0 //バ
chartable[0x1A] = 0xA5D3 //ビ
chartable[0x1B] = 0xA5D6 //ブ
chartable[0x1C] = 0xA5DC //ボ
chartable[0x1D] = 0xA5DE //マ゛ //SPECIAL (2 CHARACTERS) A5DE + A59B
chartable[0x1E] = 0xA5DF //ミ゛ //SPECIAL (2 CHARACTERS) A5DF + A59B
chartable[0x1F] = 0xA5E0 //ム゛ //SPECIAL (2 CHARACTERS) A5E0 + A59B

chartable[0x20] = 0xA5A3 //ィ゛ //SPECIAL (2 CHARACTERS) A5A3 + A59B
chartable[0x21] = 0xA542 //あ゛ //SPECIAL (2 CHARACTERS) A543 + A59B
chartable[0x22] = 0xA544 //い゛ //SPECIAL (2 CHARACTERS) A544 + A59B
chartable[0x23] = 0xA5F4 //ゔ
chartable[0x24] = 0xA548 //え゛ //SPECIAL (2 CHARACTERS) A548 + A59B
chartable[0x25] = 0xA54A //お゛ //SPECIAL (2 CHARACTERS) A54A + A59B
chartable[0x26] = 0xA54C //が
chartable[0x27] = 0xA54E //ぎ
chartable[0x28] = 0xA550 //ぐ
chartable[0x29] = 0xA552 //げ
chartable[0x2A] = 0xA554 //ご
chartable[0x2B] = 0xA556 //ざ
chartable[0x2C] = 0xA558 //じ
chartable[0x2D] = 0xA55A //ず
chartable[0x2E] = 0xA55C //ぜ
chartable[0x2F] = 0xA55E //ぞ

chartable[0x30] = 0xA560 //だ
chartable[0x31] = 0xA562 //ぢ
chartable[0x32] = 0xA565 //づ
chartable[0x33] = 0xA567 //で
chartable[0x34] = 0xA569 //ど
chartable[0x35] = 0xA56A //な゛ //SPECIAL (2 CHARACTERS) A56A + A59B
chartable[0x36] = 0xA56B //に゛ //SPECIAL (2 CHARACTERS) A56B + A59B
chartable[0x37] = 0xA56C //ぬ゛ //SPECIAL (2 CHARACTERS) A56C + A59B
chartable[0x38] = 0xA56D //ね゛ //SPECIAL (2 CHARACTERS) A56D + A59B
chartable[0x39] = 0xA56E //の゛ //SPECIAL (2 CHARACTERS) A56E + A59B
chartable[0x3A] = 0xA570 //ば
chartable[0x3B] = 0xA573 //び
chartable[0x3C] = 0xA576 //ぶ
chartable[0x3D] = 0xA579 //べ
chartable[0x3E] = 0xA57C //ぼ
chartable[0x3F] = 0xA57E //ま゛ //SPECIAL (2 CHARACTERS) A57E + A59B

chartable[0x40] = 0xA5D1 //パ
chartable[0x41] = 0xA5D4 //ピ
chartable[0x42] = 0xA5D7 //プ
chartable[0x43] = 0xA5DD //ポ
chartable[0x44] = 0xA571 //ぱ
chartable[0x45] = 0xA574 //ぴ
chartable[0x46] = 0xA577 //ぷ
chartable[0x47] = 0xA57A //ぺ
chartable[0x48] = 0xA57D //ぽ
chartable[0x49] = 0xA57E //ま゜ //SPECIAL (2 CHARACTERS) A57E + A59C
chartable[0x4A] = 0x00 //Control
chartable[0x4B] = 0x00 //Control
chartable[0x4C] = 0x00 //Control
chartable[0x4D] = 0xA5 //も゜
chartable[0x4E] = 0x00 //Control
chartable[0x4F] = 0x00 //Control


chartable[0x50] = 0x00 //Control
chartable[0x51] = 0x00 //Control
chartable[0x52] = 0x00 //Control
chartable[0x53] = 0x00 //Control
chartable[0x54] = 0x00 //Control
chartable[0x55] = 0x00 //Control
chartable[0x56] = 0x00 //Control
chartable[0x57] = 0x00 //Control
chartable[0x58] = 0x00 //Control
chartable[0x59] = 0x00 //Control
chartable[0x5A] = 0x00 //Control
chartable[0x5B] = 0x00 //Control
chartable[0x5C] = 0x00 //Control
chartable[0x5D] = 0x00 //Control
chartable[0x5E] = 0x00 //Control
chartable[0x5F] = 0x00 //Control

chartable[0x60] = 0xA541 //A
chartable[0x61] = 0xA542 //B
chartable[0x62] = 0xA543 //C
chartable[0x63] = 0xA544 //D
chartable[0x64] = 0xA545 //E
chartable[0x65] = 0xA546 //F
chartable[0x66] = 0xA547 //G
chartable[0x67] = 0xA548 //H
chartable[0x68] = 0xA549 //I
chartable[0x69] = 0xA556 //V
chartable[0x6A] = 0xA553 //S
chartable[0x6B] = 0xA543 //L
chartable[0x6C] = 0xA54D //M
chartable[0x6D] = 0xA53A //:
chartable[0x6E] = 0xA543 //ぃ
chartable[0x6F] = 0xA545 //ぅ

chartable[0x70] = 0xA50C //「
chartable[0x71] = 0xA50D //」
chartable[0x72] = 0xA50E //『
chartable[0x73] = 0xA50F //』
chartable[0x74] = 0xA5FB //・
chartable[0x75] = 0xA526 //…
chartable[0x76] = 0xA541 //ぁ
chartable[0x77] = 0xA547 //ぇ
chartable[0x78] = 0xA549 //ぉ
chartable[0x79] = 0 //Unknown unicode
chartable[0x7A] = 0xA53D //=
chartable[0x7B] = 0 //Unknown unicode
chartable[0x7C] = 0xA57C //| ****The character in GB is actually two ||
chartable[0x7D] = 0 //Unknown unicode
chartable[0x7E] = 0 //Unknown unicode
chartable[0x7F] = 0xA520 // space character

chartable[0x80] = 0xA5A2 //ア
chartable[0x81] = 0xA5A4 //イ
chartable[0x82] = 0xA5A6 //ウ
chartable[0x83] = 0xA5A8 //エ
chartable[0x84] = 0xA5AA //オ
chartable[0x85] = 0xA5AB //カ 
chartable[0x86] = 0xA5AD //キ
chartable[0x87] = 0xA5AF //ク
chartable[0x88] = 0xA5B1 //ケ
chartable[0x89] = 0xA5B3 //コ
chartable[0x8A] = 0xA5B5 //サ
chartable[0x8B] = 0xA5B7 //シ
chartable[0x8C] = 0xA5B9 //ス
chartable[0x8D] = 0xA5BB //セ
chartable[0x8E] = 0xA5BD //ソ
chartable[0x8E] = 0xA5BF //タ


chartable[0x90] = 0xA5C1 //チ
chartable[0x91] = 0xA5C4 //ツ
chartable[0x92] = 0xA5C6 //テ
chartable[0x93] = 0xA5C8 //ト
chartable[0x94] = 0xA5CA //ナ
chartable[0x95] = 0xA5CB //ニ
chartable[0x96] = 0xA5CC //ヌ
chartable[0x97] = 0xA5CD //ネ
chartable[0x98] = 0xA5CE //ノ
chartable[0x99] = 0xA5CF //ハ
chartable[0x9A] = 0xA5D2 //ヒ
chartable[0x9B] = 0xA5D5 //フ
chartable[0x9C] = 0xA5DB //ホ
chartable[0x9D] = 0xA5DE //マ
chartable[0x9E] = 0xA5DF //ミ
chartable[0x9F] = 0xA5E0 //ム

chartable[0xA0] = 0xA5E1 //メ
chartable[0xA1] = 0xA5E2 //モ
chartable[0xA2] = 0xA5E4 //ヤ
chartable[0xA3] = 0xA5E6 //ユ
chartable[0xA4] = 0xA5E8 //ヨ
chartable[0xA5] = 0xA5E9 //ラ
chartable[0xA6] = 0xA5EB //ル
chartable[0xA7] = 0xA5EC //レ
chartable[0xA8] = 0xA5ED //ロ
chartable[0xA9] = 0xA5EF //ワ
chartable[0xAA] = 0xA5F2 //ヲ
chartable[0xAB] = 0xA5F3 //ン
chartable[0xAC] = 0xA5C3 //ッ
chartable[0xAD] = 0xA5E3 //ャ
chartable[0xAE] = 0xA5E5 //ュ
chartable[0xAF] = 0xA5E7 //ョ


chartable[0xB0] = 0xA5A3 //ィ
chartable[0xB1] = 0xA542 //あ
chartable[0xB2] = 0xA544 //い
chartable[0xB3] = 0xA546 //う
chartable[0xB4] = 0xA548 //え
chartable[0xB5] = 0xA54A //お
chartable[0xB6] = 0xA54B //か
chartable[0xB7] = 0xA54D //き
chartable[0xB8] = 0xA54F //く
chartable[0xB9] = 0xA551 //け
chartable[0xBA] = 0xA553 //さ
chartable[0xBB] = 0xA555 //こ
chartable[0xBC] = 0xA557 //し
chartable[0xBD] = 0xA559 //す
chartable[0xBE] = 0xA55B //せ
chartable[0xBF] = 0xA55D //そ

chartable[0xc0] = 0xA55F //た
chartable[0xc1] = 0xA561 //ち
chartable[0xc2] = 0xA564 //つ
chartable[0xc3] = 0xA566 //て
chartable[0xc4] = 0xA568 //と
chartable[0xc5] = 0xA56A //な
chartable[0xc6] = 0xA56B //に
chartable[0xc7] = 0xA56C //ぬ
chartable[0xc8] = 0xA56D //ね
chartable[0xc9] = 0xA56E //の
chartable[0xcA] = 0xA56F //は
chartable[0xcB] = 0xA572 //ひ
chartable[0xcC] = 0xA575 //ふ
chartable[0xcD] = 0xA58 //へ
chartable[0xcE] = 0xA57B //ほ
chartable[0xcF] = 0xA57E //ま

chartable[0xD0] = 0xA57F //み
chartable[0xD1] = 0xA580 //む
chartable[0xD2] = 0xA581 //め
chartable[0xD3] = 0xA582 //も
chartable[0xD4] = 0xA584 //や
chartable[0xD5] = 0xA586 //ゆ
chartable[0xD6] = 0xA588 //よ
chartable[0xD7] = 0xA589 //ら
chartable[0xD8] = 0xA58A //り
chartable[0xD9] = 0xA58B //る
chartable[0xDA] = 0xA58C //れ
chartable[0xDB] = 0xA58D //ろ
chartable[0xDC] = 0xA58F //わ //small version isn't in GB charset (A58E)
chartable[0xDD] = 0xA592 //を
chartable[0xDE] = 0xA593 //ん
chartable[0xDF] = 0xA563 //っ

chartable[0xE0] = 0xA583 //ゃ
chartable[0xE1] = 0xA585 //ゅ
chartable[0xE2] = 0xA587 //ょ
chartable[0xE3] = 0xA5FC //ー
chartable[0xE4] = 0xA59C //゜ //needs special handling
chartable[0xE5] = 0xA59B //゛ //needs special handling
chartable[0xE6] = 0xA53F //?
chartable[0xE7] = 0xA521 //!
chartable[0xE8] = 0xA502 //。
chartable[0xE9] = 0xA5A1 //ァ
chartable[0xEA] = 0xA5A5 //ゥ
chartable[0xEB] = 0xA5A7 //ェ
chartable[0xEC] = 0xA5B7 //▷
chartable[0xED] = 0xA5B6 //▶
chartable[0xEE] = 0xA5BC //▼
chartable[0xEF] = 0xA542 //♂


chartable[0xF0] = 0xA5A5 //円 //Yen sign, conflicts with ゥ (0xEA in GB table)
chartable[0xF1] = 0xA5D7 //×
chartable[0xF2] = 0xA52E //.
chartable[0xF3] = 0xA52F //'/'
chartable[0xF4] = 0xA5A9 //ォ
chartable[0xF5] = 0xA540 //♀
chartable[0xF6] = 0xA530 //0
chartable[0xF7] = 0xA531 //1
chartable[0xF8] = 0xA532 //2
chartable[0xF9] = 0xA533 //3
chartable[0xFA] = 0xA534 //4
chartable[0xFB] = 0xA535 //5
chartable[0xFC] = 0xA536 //6
chartable[0xFD] = 0xA537 //7
chartable[0xFE] = 0xA538 //8
chartable[0xFF] = 0xA539 //9
*/