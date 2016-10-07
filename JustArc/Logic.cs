using System;
using Microsoft.Xna.Framework;

namespace JustArc
{
	public class Logic
	{
        public int playerSprWidth = 32;
        public int playerSprHeight = 32;
        public int hpSprWidth = 128;
        public int hpSprHeight = 34;

        int num1, num2, num3;
		string str1, str2, str3;
        const string STRI = "lol";
		string[] str = {"HIT", "BANG", "TOUCH", "BLOWUP" };
        string[] string2 = {"HIS", "HERE", "YOUR", "THEIR" };
        string[] string3 = {"DOG", "CAT", "MOM", "DAD"};
        //private const string[] COMBO = {
        //    "HIT.HIS.CAT","HIT.HIS.MOM","HIT.HIS.DAD","HIT.HERE.DOG", "HIT.HERE.CAT", "HIT.HERE.MOM", "HIT.HERE.DAD", "HIT.YOUR.DOG", "HIT.YOUR.CAT", "HIT.YOUR.MOM", "HIT.YOUR.DAD", "HIT.THEIR.DOG", "HIT.THEIR.CAT", "HIT.THEIR.MOM", "HIT.THEIR.DAD",
        //    "BANG.HIS.DOG","BANG.HIS.CAT","BANG.HIS.MOM","BANG.HIS.DAD","BANG.HERE.DOG", "BANG.HERE.CAT", "BANG.HERE.MOM", "BANG.HERE.DAD", "BANG.YOUR.DOG", "BANG.YOUR.CAT", "BANG.YOUR.MOM", "BANG.YOUR.DAD", "BANG.THEIR.DOG", "BANG.THEIR.CAT", "BANG.THEIR.MOM", "BANG.THEIR.DAD",
        //    "TOUCH.HIS.DOG","TOUCH.HIS.CAT","TOUCH.HIS.MOM","TOUCH.HIS.DAD","TOUCH.HERE.DOG", "TOUCH.HERE.CAT", "TOUCH.HERE.MOM", "TOUCH.HERE.DAD", "TOUCH.YOUR.DOG", "TOUCH.YOUR.CAT", "TOUCH.YOUR.MOM", "TOUCH.YOUR.DAD", "TOUCH.THEIR.DOG", "TOUCH.THEIR.CAT", "TOUCH.THEIR.MOM", "TOUCH.THEIR.DAD",
        //    "BLOWUP.HIS.DOG","BLOWUP.HIS.CAT","BLOWUP.HIS.MOM","BLOWUP.HIS.DAD","BLOWUP.HERE.DOG", "BLOWUP.HERE.CAT", "BLOWUP.HERE.MOM", "BLOWUP.HERE.DAD", "BLOWUP.YOUR.DOG", "BLOWUP.YOUR.CAT", "BLOWUP.YOUR.MOM", "BLOWUP.YOUR.DAD", "BLOWUP.THEIR.DOG", "BLOWUP.THEIR.CAT", "BLOWUP.THEIR.MOM", "BLOWUP.THEIR.DAD",
        //};

    int hp1, hp2;
        public static bool hit = false;
        public Point playerSprSize = new Point(4, 4);
        public Point playerSprFrame = new Point(0, 0);
        public Point hpSprSize = new Point(1, 10);
        public Point hpSprFrame = new Point(0, 0);
        Random random = new Random();
        public Logic ()
		{
            
		}

		public String showWords(int index){
			
			num1 = random.Next (0, 4);
			num2 = random.Next (0, 4);
			num3 = random.Next (0, 4);
            if(index == 1) str1 = str [num1];
            if (index == 2) str1 = string2 [num2];
            if (index == 3) str1 = string3 [num3];
            return str1;
            
		}

    }
}

