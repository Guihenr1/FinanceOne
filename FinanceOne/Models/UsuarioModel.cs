using FinanceOne.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceOne.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "informe sua data de nascimento!")]
        public DateTime Data_Nascimento { get; set; }
                
        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL='{Email}' AND SENHA='{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            if(dt != null)
            {
                if(dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    Nome = dt.Rows[0]["NOME"].ToString();
                    Data_Nascimento = DateTime.Parse(dt.Rows[0]["DATA_NASCIMENTO"].ToString());
                    return true;
                }
            }
            return false;
        }
    }
}
