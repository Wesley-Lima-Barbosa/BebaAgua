using System;
using System.Collections.Generic;
using System.Text;

namespace BebaAgua.Classes
{
    public class PadraoUsuario
    {

        public static Images.Models.Usuario usuario()
        {
        Images.Models.Usuario user = new Images.Models.Usuario();
            user.IntervaloNotificacao = 15;
            user.Led = true;
            user.Peso = 70; 
            user.PrefLiquido = 1; //1 = ml - 2 = oz
            user.PrefPeso = 1; //1 = kg - 2 = lb
            user.Vibracao = true;
            user.IntervaloNotificacao = 15;
            return user;
        }
    }
}
