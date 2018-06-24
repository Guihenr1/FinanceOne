using FinanceOne.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceOne.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Descrição!")]
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public PlanoContaModel()
        {

        }

        //Recebe o conceito para acesso das variaveis de sessão.
        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<PlanoContaModel> ListaPlanoContas()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();
            PlanoContaModel Item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID={id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Item = new PlanoContaModel();
                Item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                Item.Descricao = dt.Rows[i]["DESCRICAO"].ToString();
                Item.Tipo = dt.Rows[i]["TIPO"].ToString();
                Item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());
                lista.Add(Item);
            }
            return lista;
        }

        public PlanoContaModel CarregarRegistro(int? id)
        {
            PlanoContaModel Item = new PlanoContaModel();

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID={id_usuario_logado} AND ID = {id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            Item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            Item.Descricao = dt.Rows[0]["DESCRICAO"].ToString();
            Item.Tipo = dt.Rows[0]["TIPO"].ToString();
            Item.Usuario_Id = int.Parse(dt.Rows[0]["USUARIO_ID"].ToString());
            
            return Item;
        }

        public void Insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "";
            if (Id == 0)
            {
               sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO, TIPO, USUARIO_ID) VALUES ('{Descricao}', '{Tipo}', '{id_usuario_logado}')";
            }
            else
            {
                sql = $"UPDATE PLANO_CONTAS SET DESCRICAO = '{Descricao}', TIPO = '{Tipo}' WHERE USUARIO_ID = '{id_usuario_logado}' AND ID = '{Id}'";
            }
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void Excluir(int id_conta)
        {
            new DAL().ExecutarComandoSQL("DELETE FROM PLANO_CONTAS WHERE ID = " + id_conta);
        }
    }
}
