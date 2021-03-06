﻿using FinanceOne.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceOne.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Data!")]
        public string Data { get; set; }

        public string Tipo { get; set; }

        public double Valor { get; set; }

        [Required(ErrorMessage = "Informe a Descrição!")]
        public string Descricao { get; set; }

        public int Conta_Id { get; set; }
        public string NomeConta { get; set; }

        public int Plano_Contas_Id { get; set; }
        public string DescricaoPlanoConta { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoModel()
        {

        }

        //Recebe o conceito para acesso das variaveis de sessão.
        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<TransacaoModel> ListaTransacao()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel Item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "select t.Id, t.Data, t.Tipo, t.Valor, t.Descricao as historico," +
                            " t.Conta_Id, c.Nome as conta, t.Plano_Contas_Id, p.Descricao as plano_conta " +
                            " from transacao as t inner join conta c " +
                            " on t.Conta_Id = c.Id inner join Plano_Contas as p " +
                            " on t.Plano_Contas_Id = p.Id " +
                            $" where t.usuario_id = {id_usuario_logado} order by t.data desc limit 10;";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Item = new TransacaoModel();
                Item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                Item.Data = DateTime.Parse(dt.Rows[i]["Data"].ToString()).ToString("dd/MM/yyyy");
                Item.Tipo = dt.Rows[i]["Tipo"].ToString();
                Item.Descricao = dt.Rows[i]["historico"].ToString();
                Item.Valor = double.Parse(dt.Rows[i]["Valor"].ToString());
                Item.Conta_Id = int.Parse(dt.Rows[i]["Conta_Id"].ToString());
                Item.NomeConta = dt.Rows[i]["conta"].ToString();
                Item.Plano_Contas_Id = int.Parse(dt.Rows[i]["Plano_Contas_Id"].ToString());
                Item.DescricaoPlanoConta = dt.Rows[i]["plano_conta"].ToString();
                lista.Add(Item);
            }
            return lista;
        }
    }
}
