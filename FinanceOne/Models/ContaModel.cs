using FinanceOne.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceOne.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }
        IHttpContextAccessor HttpContextAccessor;

        public ContaModel()
        {

        }

        //Recebe o conceito para acesso das variaveis de sessão.
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel Item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE USUARIO_ID={id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Item = new ContaModel();
                Item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                Item.Nome = dt.Rows[i]["NOME"].ToString();
                Item.Saldo = double.Parse(dt.Rows[i]["SALDO"].ToString());
                Item.Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());
                lista.Add(Item);
            }
            return lista;
        }
    }
}
