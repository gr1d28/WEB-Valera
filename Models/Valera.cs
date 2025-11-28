namespace Valera.Models
{
    public class Valera
    {
        public int Id { get; set; } = 0;
        public int Health { get; set; } = 100;
        public int Mana { get; set; } = 0;      //алкоголь в крови
        public int Cheerfulness { get; set; } = 0;  //жизнерадостность
        public int Fatigue { get; set; } = 0;       //усталость
        public int Money { get; set; } = 512;   //на неделю
        public int UserId { get; set; }

        public Valera() { }
        public Valera(int id, int health, int mana, int cheerfulness, int fatigue, int money)
        {
            Id = id;
            Health = health;
            Mana = mana;
            Cheerfulness = cheerfulness;
            Fatigue = fatigue;
            Money = money;
        }

        public void SetHealth(int value)
        {
            Health = Math.Clamp(value, 0, 100);
        }

        public void SetMana(int value)
        {
            Mana = Math.Clamp(value, 0, 100);
        }

        public void SetCheerfulness(int value)
        {
            Cheerfulness = Math.Clamp(value, -10, 10);
        }

        public void SetFatigue(int value)
        {
            Fatigue = Math.Clamp(value, 0, 100);
        }

        public void SetMoney(int value)
        {
            Money = value;
        }


        private void CheckFields()
        {
            if (Health < 0)
                Health = 0;
            if (Health > 100)
                Health = 100;
            if (Mana < 0)
                Mana = 0;
            if (Mana > 100)
                Mana = 100;
            if (Cheerfulness < -10)
                Cheerfulness = -10;
            if (Cheerfulness > 10)
                Cheerfulness = 10;
            if (Fatigue < 0)
                Fatigue = 0;
            if (Fatigue > 100)
                Fatigue = 100;
        }

        public void Work()
        {
            if (!(Mana < 50 && Fatigue < 10))
                return;
            Cheerfulness -= 5;
            Mana -= 30;
            Money += 100;
            Fatigue += 70;
            //CheckFields();
        }

        public void ContemplateNature()
        {
            Cheerfulness++;
            Mana -= 10;
            Fatigue += 10;
            //CheckFields();
        }

        public void DrinkingWineWatchingSeries()
        {
            Cheerfulness--;
            Mana += 30;
            Fatigue += 10;
            Health -= 5;
            Money -= 20;
            //CheckFields();
        }

        public void Bar()
        {
            Cheerfulness++;
            Mana += 60;
            Fatigue += 40;
            Health -= 10;
            Money -= 100;
            //CheckFields();
        }

        public void DrinkingWithFreinds()
        {
            Cheerfulness += 5;
            Health -= 80;
            Mana += 90;
            Fatigue += 80;
            Money -= 150;
            //CheckFields();
        }

        public void SingingSubway()
        {
            Cheerfulness++;
            Money += (Mana > 40 && Mana < 70) ? 60 : 10;
            Mana += 10;
            Fatigue += 20;
            //CheckFields();
        }

        public void Sleep()
        {
            if (Mana > 70)
                Cheerfulness -= 3;
            if (Mana < 30)
                Health += 90;
            Mana -= 50;
            Fatigue -= 70;
            //CheckFields();
        }
    }
}
