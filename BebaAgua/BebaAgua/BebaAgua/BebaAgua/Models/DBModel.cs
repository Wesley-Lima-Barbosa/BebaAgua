using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BebaAgua.Images.Models
{


    public class Usuario
    {


        //Cria o Id automatico e configura a chave primaria
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public double Peso { get; set; }

        public double Meta
        {
            get
            {
                return Peso * 30;
            }
        }
        public bool Notificacao { get; set; }

        public bool Vibracao { get; set; }

        public bool Led { get; set; }

        public double IntervaloNotificacao { get; set; }

        public double PrefPeso { get; set; }

        public double PrefLiquido { get; set; }

    }

    public class Historico
    {
        //Cria o Id automatico e configura a chave primaria
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public DateTime Horario { get; set; }

        public double Quantidade { get; set; }

        public double Meta { get; set; }


        [IgnoreAttribute]
        public string Image
        {
            get
            {

                if (Quantidade < 240)
                    return "copohist.png";
                else if (Quantidade > 239 && Quantidade < 301)
                    return "xicarahist.png";
                else if (Quantidade > 300 && Quantidade < 499)
                    return "canecahist.png";
                else if (Quantidade > 499 && Quantidade < 701)
                    return "garrafahist.png";
                else
                    return "Jarra.png";
            }
        }
        [Ignore]
        public bool SwitchEnable { get; set; }
        [Ignore]
        public string NewQuant
        {
            get
            {
                Usuario user = App.Banco().usuario.Consultar(1).Result;
                if (user.PrefLiquido == 1)
                    return Quantidade.ToString("0") + " ml";
                else
                    return (Quantidade / 30).ToString("0.00") + " oz";
            }
        }
    }

        public class Alarme
        {
            //Cria o Id automatico e configura a chave primaria
            [PrimaryKey, AutoIncrement]

            public int ID { get; set; }

            public string HorarioInicio { get; set; }

            public string HorarioFim { get; set; }

            [Ignore]
            public bool SwitchEnable { get; set; }

        }

    }

