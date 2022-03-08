using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Preferencias_BLL;
using PreferenciasVO_Model;
using EXCEL = Microsoft.Office.Interop.Excel;
using EMAIL = Microsoft.Office.Interop.Outlook;

namespace exercicioFamiliaresCompleto_1_23112020
{
    public partial class frmExercicioFamiliaresCompleto_1_23112020 : Form
    {
        int intValorID, intValorCod, intPrefFamCod, intPrefFamID;
        string strValorAntigo, strNomeAntigo;

        bool bolPreferenciaInserida, bolFamiliarInserido, bolPrefFamInserido;

        Preferencia objPreferencia;
        PreferenciaVO objPreferenciaVO;
        Familiar objFamiliar;
        FamiliarVO objFamiliarVO;
        PreferenciasDeFamiliar objPreferenciasDeFamiliar;
        PreferenciasDeFamiliarVO objPreferenciasDeFamiliarVO;

        EXCEL._Application objExcelAplicacao;
        EXCEL.Workbook objExcelArquivo;
        EXCEL.Worksheet objExcelPlanilha;
        EXCEL.Range objExcelPlanilhaCabecalho;
        EXCEL.Range objExcelPlanilhaDados;

        EMAIL.Application objOutlookAplicacao;
        EMAIL.MailItem objOutlookItemMailMensagem;
        EMAIL.OlAttachmentType objOutlookAnexoTipo;
        string[] arrayOutlookArquivosAnexos = new string[0];
        long lngOutlookArquivosAnexosPosicao;
        string strOutLookArquivosAnexosDisplayName;

        public frmExercicioFamiliaresCompleto_1_23112020()
        {
            InitializeComponent();
        }

        private void dtgdvwPreferencias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            strValorAntigo = dtgdvwPreferencias.CurrentRow.Cells["Descricao"].Value.ToString();

            if (!string.IsNullOrEmpty(dtgdvwPreferencias.CurrentRow.Cells["ID"].Value.ToString()))
            {
                intValorID = Convert.ToInt32(dtgdvwPreferencias.CurrentRow.Cells["ID"].Value.ToString());
            }
        }

        private void dtgdvwFamiliares_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            strNomeAntigo = dtgdvwFamiliares.CurrentRow.Cells["Nome"].Value.ToString();

            if (!string.IsNullOrEmpty(dtgdvwFamiliares.CurrentRow.Cells["Cod"].Value.ToString()))
            {
                intValorCod = Convert.ToInt32(dtgdvwFamiliares.CurrentRow.Cells["Cod"].Value.ToString());
            }
        }

        private void btnDesvCond_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clique em ok ou cancelar", "Desvio condicional", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Você clicou em ok");
            }
            else
            {
                MessageBox.Show("Você clicou em cancelar");
            }
        }

        private void btnImpTxtWhile_Click(object sender, EventArgs e)
        {
            try
            {
                lstbxPreferencias.Items.Clear();
                objPreferencia = new Preferencia();
                lstbxPreferencias.Items.AddRange(objPreferencia.ImpTxtWhile().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImpBDCon_Click(object sender, EventArgs e)
        {
            try
            {
                lstbxPreferencias.Items.Clear();
                objPreferencia = new Preferencia();
                lstbxPreferencias.Items.AddRange(objPreferencia.ImpBDCon().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImpBDDesc_Click(object sender, EventArgs e)
        {
            try
            {
                lstbxPreferencias.Items.Clear();
                objPreferencia = new Preferencia();
                lstbxPreferencias.Items.AddRange(objPreferencia.ImpBDDesc().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConsultarBD_Click(object sender, EventArgs e)
        {
            ConsultarBD();
        }
        public void ConsultarBD(int ? intID = null, string strParDescricaoPreferencias = null)
        {
            try
            {
                objPreferenciaVO = new PreferenciaVO();
                if (intID != null)
                {
                    objPreferenciaVO.setID(Convert.ToInt32(intID));
                }
                objPreferenciaVO.setDescricao(strParDescricaoPreferencias);

                objPreferencia = new Preferencia();
                bndsrcPreferencias.DataSource = objPreferencia.ConsultarBD(objPreferenciaVO);
                dtgdvwPreferencias.DataSource = bndsrcPreferencias;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ConsultarBDFamiliares(int? intCod = null, string strParNomeFamiliares = null)
        {
            try
            {
                objFamiliarVO = new FamiliarVO();
                if (intCod != null)
                {
                    objFamiliarVO.setCod(Convert.ToInt32(intCod));
                }
                objFamiliarVO.setNome(strParNomeFamiliares);

                objFamiliar = new Familiar();
                bndsrcFamiliares.DataSource = objFamiliar.ConsultarBDFamiliar(objFamiliarVO);

                dtgdvwFamiliares.Columns.Clear();
                dtgdvwFamiliares.DataSource = null;

                dtgdvwFamiliares.Columns.Add("Cod", "Cod");
                dtgdvwFamiliares.Columns["Cod"].DataPropertyName = "Cod";

                dtgdvwFamiliares.Columns.Add("Nome", "Nome");
                dtgdvwFamiliares.Columns["Nome"].DataPropertyName = "Nome";

                DataGridViewComboBoxColumn objColunaComboBoxSexoDoGrid = new DataGridViewComboBoxColumn();
                objColunaComboBoxSexoDoGrid.Name = "Sexo";
                objColunaComboBoxSexoDoGrid.ValueType = typeof(string);
                objColunaComboBoxSexoDoGrid.HeaderText = "Sexo";
                objColunaComboBoxSexoDoGrid.Items.Add("FEMININO");
                objColunaComboBoxSexoDoGrid.Items.Add("MASCULINO");
                objColunaComboBoxSexoDoGrid.Items.Add("INDETERMINADO");
                objColunaComboBoxSexoDoGrid.DataPropertyName = "Sexo";

                dtgdvwFamiliares.Columns.Add(objColunaComboBoxSexoDoGrid);
                dtgdvwFamiliares.Columns["Sexo"].DataPropertyName = "Sexo";

                dtgdvwFamiliares.Columns.Add("Idade", "Idade");
                dtgdvwFamiliares.Columns["Idade"].DataPropertyName = "Idade";

                dtgdvwFamiliares.Columns.Add("Ganho_Total_Mensal", "Ganho Total Mensal");
                dtgdvwFamiliares.Columns["Ganho_Total_Mensal"].DataPropertyName = "Ganho_Total_Mensal";

                dtgdvwFamiliares.Columns.Add("Gasto_Total_Mensal", "Gasto Total Mensal");
                dtgdvwFamiliares.Columns["Gasto_Total_Mensal"].DataPropertyName = "Gasto_Total_Mensal";

                dtgdvwFamiliares.Columns.Add("Observacao", "Observacao");
                dtgdvwFamiliares.Columns["Observacao"].DataPropertyName = "Observacao";

                dtgdvwFamiliares.DataSource = bndsrcFamiliares;
                dtgdvwFamiliares.AllowUserToAddRows = false;

                cmbbxPrefFamFamiliar.DataSource = null;
                cmbbxPrefFamFamiliar.Items.Clear();

                cmbbxPrefFamFamiliar.DisplayMember = "Nome";
                cmbbxPrefFamFamiliar.ValueMember = "Cod";
                cmbbxPrefFamFamiliar.DataSource = bndsrcFamiliares.DataSource;
                //cmbbxPrefFamFamiliar.DisplayMember = "Nome";
                //cmbbxPrefFamFamiliar.ValueMember = "Cod";
                cmbbxPrefFamFamiliar.SelectedIndex = Convert.ToInt32(intCod > 0 ? intCod -1 : 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ConsultarBDPrefFam(int? intCod = null,
                                        int? intID = null,
                                        string strNome = null,
                                        string strDescricao = null)
        {
            try
            {
                objPreferenciasDeFamiliarVO = new PreferenciasDeFamiliarVO();

                objPreferenciasDeFamiliarVO.ObjFamiliarVO = new FamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod = Convert.ToInt32(intCod == null ? 0 : intCod);
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Nome = strNome;

                objPreferenciasDeFamiliarVO.ObjPreferenciaVO = new PreferenciaVO();
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID = Convert.ToInt32(intID == null ? 0 : intID);
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.Descricao = strDescricao;

                objPreferenciasDeFamiliar = new PreferenciasDeFamiliar();
                bndsrcPrefFam.DataSource = objPreferenciasDeFamiliar.ConsultarBD(objPreferenciasDeFamiliarVO);

                dtgdvwPrefFam.DataSource = null;
                dtgdvwPrefFam.Columns.Clear();
                dtgdvwPrefFam.AllowUserToAddRows = false;
                    
                DataGridViewComboBoxColumn objColunaComboBoxFamiliarDoGrid = new DataGridViewComboBoxColumn();
                objColunaComboBoxFamiliarDoGrid.DataSource = bndsrcFamiliares.DataSource;
                objColunaComboBoxFamiliarDoGrid.Name = "Cod";
                objColunaComboBoxFamiliarDoGrid.ValueType = typeof(int);
                objColunaComboBoxFamiliarDoGrid.HeaderText = "Cod";
                objColunaComboBoxFamiliarDoGrid.DisplayMember = "Nome";
                objColunaComboBoxFamiliarDoGrid.ValueMember = "Cod";
                objColunaComboBoxFamiliarDoGrid.DataPropertyName = "Cod";

                dtgdvwPrefFam.Columns.Add(objColunaComboBoxFamiliarDoGrid);
                dtgdvwPrefFam.Columns["Cod"].DataPropertyName = "Cod";

                DataGridViewComboBoxColumn objColunaComboBoxPreferenciasDoGrid = new DataGridViewComboBoxColumn();
                objPreferencia = new Preferencia();
                objColunaComboBoxPreferenciasDoGrid.DataSource = objPreferencia.ConsultarBD(objPreferenciaVO);
                objColunaComboBoxPreferenciasDoGrid.Name = "ID";
                objColunaComboBoxPreferenciasDoGrid.ValueType = typeof(int);
                objColunaComboBoxPreferenciasDoGrid.HeaderText = "ID";
                objColunaComboBoxPreferenciasDoGrid.DisplayMember = "Descricao";
                objColunaComboBoxPreferenciasDoGrid.ValueMember = "ID";
                objColunaComboBoxPreferenciasDoGrid.DataPropertyName = "ID";

                dtgdvwPrefFam.Columns.Add(objColunaComboBoxPreferenciasDoGrid);
                dtgdvwPrefFam.Columns["ID"].DataPropertyName = "ID";

                dtgdvwPrefFam.Columns.Add("Intensidade", "Intensidade");
                dtgdvwPrefFam.Columns["Intensidade"].DataPropertyName = "Intensidade";

                dtgdvwPrefFam.Columns.Add("observacao", "observacao");
                dtgdvwPrefFam.Columns["observacao"].DataPropertyName = "observacao";

                dtgdvwPrefFam.DataSource = bndsrcPrefFam.DataSource;

                bndnavcmbbxPrefFamPesquisa.Items.Clear();
                bndnavcmbbxPrefFamPesquisa.Items.Add("0-");

                DataTable objTabelaPreferencias = objPreferencia.ConsultarBD(objPreferenciaVO);


                foreach (DataRow drItemdaTabelaPreferencias in objTabelaPreferencias.Rows)
                {
                    bndnavcmbbxPrefFamPesquisa.Items.Add(drItemdaTabelaPreferencias["ID"].ToString() + " - " + drItemdaTabelaPreferencias["Descricao"].ToString());
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void dtgdvwPreFamRefresh()
        {
            try
            {
                ConsultarBDPrefFam(Convert.ToInt32(cmbbxPrefFamFamiliar.SelectedValue.ToString()), 
                                    null, 
                                    cmbbxPrefFamFamiliar.Text, 
                                    null);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInserirBD_Click(object sender, EventArgs e)
        {
            InserirBD(dtgdvwPreferencias.CurrentRow.Cells["Descricao"].EditedFormattedValue.ToString());
            ConsultarBD();
        }
        public void InserirBD(string strPreferenciaInserida)
        {
            try
            {
                objPreferenciaVO = new PreferenciaVO();
                objPreferenciaVO.setDescricao(strPreferenciaInserida);

                objPreferencia = new Preferencia();
                if (objPreferencia.InserirBD(objPreferenciaVO))
                {
                    MessageBox.Show("Registro inserido");
                }
                else
                {
                    MessageBox.Show("Problemas na inserção do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        public void InserirBDFamiliares(string strNome,
                                        string strSexo = null,
                                        int? intIdade = null,
                                        double? dblGanho_Total_Mensal = null,
                                        double? dblGasto_Total_Mensal = null,
                                        string strObservacao = null)
        {
            try
            {
                objFamiliarVO = new FamiliarVO();
                objFamiliarVO.setNome(strNome);
                objFamiliarVO.setSexo(strSexo);
                objFamiliarVO.setIdade(Convert.ToInt32(intIdade == null ? 0 : intIdade));
                objFamiliarVO.setGanho_Total_Mensal(Convert.ToDouble(dblGanho_Total_Mensal == null ? 0 : dblGanho_Total_Mensal));
                objFamiliarVO.setGasto_Total_Mensal(Convert.ToDouble(dblGasto_Total_Mensal == null ? 0 : dblGasto_Total_Mensal));
                objFamiliarVO.setObservacao(strObservacao);

                objFamiliar = new Familiar();
                if (objFamiliar.InserirBDFamiliar(objFamiliarVO))
                {
                    MessageBox.Show("Registro inserido");
                }
                else
                {
                    MessageBox.Show("Problemas na inserção do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InserirBDPrefFam(int intCod,
                                    int intID,
                                    float fltIntensidade,
                                    string strNome = null,
                                    string strDescricao = null,
                                    string strObservacao = null)
        {
            try
            {
                objPreferenciasDeFamiliarVO = new PreferenciasDeFamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO = new FamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod = intCod;
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Nome = strNome;

                objPreferenciasDeFamiliarVO.ObjPreferenciaVO = new PreferenciaVO();
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID = intID;
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.Descricao = strDescricao;

                objPreferenciasDeFamiliarVO.setIntensidade(fltIntensidade);
                objPreferenciasDeFamiliarVO.setObservacao(strObservacao);

                objPreferenciasDeFamiliar = new PreferenciasDeFamiliar();
                if (objPreferenciasDeFamiliar.InserirBD(objPreferenciasDeFamiliarVO))
                {
                    MessageBox.Show("Registro inserido");
                }
                else
                {
                    MessageBox.Show("Problemas na inserção do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnExcluirBD_Click(object sender, EventArgs e)
        {
            ExcluirBD(Convert.ToInt32(dtgdvwPreferencias.CurrentRow.Cells["ID"].Value.ToString()));
            ConsultarBD();
        }
        public void ExcluirBD(int intID)
        {
            try
            {
                objPreferenciaVO = new PreferenciaVO();
                objPreferenciaVO.setID(intID);

                objPreferencia = new Preferencia();
                if (objPreferencia.ExcluirBD(objPreferenciaVO))
                {
                    MessageBox.Show("Registro excluído");
                }
                else
                {
                    MessageBox.Show("Problemas na esclusão do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ExcluirBDFamiliares(int intCod)
        {
            try
            {
                objFamiliarVO = new FamiliarVO();
                objFamiliarVO.setCod(intCod);

                objFamiliar = new Familiar();
                if (objFamiliar.ExcluirBDFamiliar(objFamiliarVO))
                {
                    MessageBox.Show("Registro excluído");
                }
                else
                {
                    MessageBox.Show("Problemas na esclusão do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ExcluirBDPrefFam(
                                        int intCod,
                                        int intID,
                                        string strNome = null,
                                        string strDescricao = null)
        {
            try
            {
                objPreferenciasDeFamiliarVO = new PreferenciasDeFamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO = new FamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.setCod(intCod);
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Nome = strNome;

                objPreferenciasDeFamiliarVO.ObjPreferenciaVO = new PreferenciaVO();
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID = intID;
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.Descricao = strDescricao;

                objPreferenciasDeFamiliar = new PreferenciasDeFamiliar();
                if (objPreferenciasDeFamiliar.ExcluirBD(objPreferenciasDeFamiliarVO))
                {
                    MessageBox.Show("Registro excluído");
                }
                else
                {
                    MessageBox.Show("Problemas na esclusão do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAlterarBD_Click(object sender, EventArgs e)
        {
            AlterarBD(intValorID, dtgdvwPreferencias.CurrentRow.Cells["Descricao"].EditedFormattedValue.ToString());
            ConsultarBD();
        }
        public void AlterarBD(int intID, string strPreferenciaNova)
        {
            try
            {
                objPreferenciaVO = new PreferenciaVO();
                objPreferenciaVO.setID(intID);
                objPreferenciaVO.setDescricao(strPreferenciaNova);

                objPreferencia = new Preferencia();
                if (objPreferencia.AlterarBD(objPreferenciaVO))
                {
                    MessageBox.Show("Registro alterado");
                }
                else
                {
                    MessageBox.Show("Problemas na alteração do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AlterarBDFamiliares(int intCod, 
                                        string strNome, 
                                        string strSexo = null, 
                                        int ? intIdade = null, 
                                        double ? dblGanho_Total_Mensal = null, 
                                        double ? dblGasto_Total_Mensal = null, 
                                        string strObservacao = null)
        {
            try
            {
                objFamiliarVO = new FamiliarVO();
                objFamiliarVO.setCod(intCod);
                objFamiliarVO.setNome(strNome);
                objFamiliarVO.setSexo(strSexo);
                objFamiliarVO.setIdade(Convert.ToInt32(intIdade == null ? 0 : intIdade));
                objFamiliarVO.setGanho_Total_Mensal(Convert.ToDouble(dblGanho_Total_Mensal == null ? 0 : dblGanho_Total_Mensal));
                objFamiliarVO.setGasto_Total_Mensal(Convert.ToDouble(dblGasto_Total_Mensal == null ? 0 : dblGasto_Total_Mensal));
                objFamiliarVO.setObservacao(strObservacao);

                objFamiliar = new Familiar();
                if (objFamiliar.AlterarBDFamiliar(objFamiliarVO))
                {
                    MessageBox.Show("Registro alterado");
                }
                else
                {
                    MessageBox.Show("Problemas na alteração do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AlterarBDPrefFam(int intCod,
                                    int intID,
                                    float fltIntensidade,
                                    string strNome = null,
                                    string strDescricao = null,
                                    string strObservacao = null)
        {
            try
            {
                objPreferenciasDeFamiliarVO = new PreferenciasDeFamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO = new FamiliarVO();
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod = intCod;
                objPreferenciasDeFamiliarVO.ObjFamiliarVO.Nome = strNome;

                objPreferenciasDeFamiliarVO.ObjPreferenciaVO = new PreferenciaVO();
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID = intID;
                objPreferenciasDeFamiliarVO.ObjPreferenciaVO.Descricao = strDescricao;

                objPreferenciasDeFamiliarVO.setIntensidade(fltIntensidade);
                objPreferenciasDeFamiliarVO.setObservacao(strObservacao);

                objPreferenciasDeFamiliar = new PreferenciasDeFamiliar();
                if (objPreferenciasDeFamiliar.AlterarBD(objPreferenciasDeFamiliarVO))
                {
                    MessageBox.Show("Registro alterado");
                }
                else
                {
                    MessageBox.Show("Problemas na alteração do Registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bndnavbtnPesquisarPreferencias_Click(object sender, EventArgs e)
        {
            ConsultarBD(null, bndnavtxtPesquisarPreferencias.Text);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bolPreferenciaInserida = true;
        }

        private void bndnavbtnConfirmarPreferencias_Click(object sender, EventArgs e)
        {
            if (bolPreferenciaInserida)
            {
                if (MessageBox.Show("Confirma a inserção da preferência " + dtgdvwPreferencias.CurrentRow.Cells["Descricao"].EditedFormattedValue.ToString(), "Inserção no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    InserirBD(dtgdvwPreferencias.CurrentRow.Cells["Descricao"].EditedFormattedValue.ToString());
                }
                bolPreferenciaInserida = false;
            }
            else
            {
                if (MessageBox.Show("Confirma a alteração da preferência " + strValorAntigo + " pela preferência nova " + dtgdvwPreferencias.CurrentRow.Cells["Descricao"].EditedFormattedValue.ToString(), "Alteração no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    AlterarBD(intValorID ,dtgdvwPreferencias.CurrentRow.Cells["Descricao"].EditedFormattedValue.ToString());
                }
            }
            ConsultarBD();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a exclusão da preferência " + strValorAntigo, "Exclusão no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ExcluirBD(intValorID);
            }
            ConsultarBD();
        }

        private void frmExercicioFamiliaresCompleto_1_23112020_Load(object sender, EventArgs e)
        {
            ConsultarBD();
            ConsultarBDFamiliares();
            dtgdvwPreFamRefresh();
        }

        private void bndnavbtnPesquisaFamiliares_Click(object sender, EventArgs e)
        {
            ConsultarBDFamiliares(null ,bndnavtxtPesquisaFamiliares.Text);
        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            bolFamiliarInserido = true;
        }

        private void bndnavbtnConfirmaFamiliares_Click(object sender, EventArgs e)
        {
            if (bolFamiliarInserido)
            {
                if (MessageBox.Show("Confirma a inserção do familiar " + dtgdvwFamiliares.CurrentRow.Cells["Nome"].EditedFormattedValue.ToString(), "Inserção no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    InserirBDFamiliares(dtgdvwFamiliares.CurrentRow.Cells["Nome"].EditedFormattedValue.ToString(),
                                        dtgdvwFamiliares.CurrentRow.Cells["Sexo"].EditedFormattedValue.ToString(),
                                        dtgdvwFamiliares.CurrentRow.Cells["Idade"].EditedFormattedValue.ToString() == string.Empty ? 0 : Convert.ToInt32(dtgdvwFamiliares.CurrentRow.Cells["Idade"].EditedFormattedValue.ToString()),
                                        dtgdvwFamiliares.CurrentRow.Cells["Ganho_Total_Mensal"].EditedFormattedValue.ToString() == string.Empty ? 0 : Convert.ToDouble(dtgdvwFamiliares.CurrentRow.Cells["Ganho_Total_Mensal"].EditedFormattedValue.ToString()),
                                        dtgdvwFamiliares.CurrentRow.Cells["Gasto_Total_Mensal"].EditedFormattedValue.ToString() == string.Empty ? 0 : Convert.ToDouble(dtgdvwFamiliares.CurrentRow.Cells["Gasto_Total_Mensal"].EditedFormattedValue.ToString()),
                                        dtgdvwFamiliares.CurrentRow.Cells["Observacao"].EditedFormattedValue.ToString());
                }
                bolFamiliarInserido = false;
            }
            else
            {
                if (MessageBox.Show("Confirma a alteração do familiar " + strNomeAntigo + " pelo familiar novo " + dtgdvwFamiliares.CurrentRow.Cells["Nome"].EditedFormattedValue.ToString(), "Alteração no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    AlterarBDFamiliares(intValorCod ,
                                        dtgdvwFamiliares.CurrentRow.Cells["Nome"].EditedFormattedValue.ToString(),
                                        dtgdvwFamiliares.CurrentRow.Cells["Sexo"].EditedFormattedValue.ToString(),
                                        dtgdvwFamiliares.CurrentRow.Cells["Idade"].EditedFormattedValue.ToString() == string.Empty ? 0 : Convert.ToInt32(dtgdvwFamiliares.CurrentRow.Cells["Idade"].EditedFormattedValue.ToString()),
                                        dtgdvwFamiliares.CurrentRow.Cells["Ganho_Total_Mensal"].EditedFormattedValue.ToString() == string.Empty ? 0 : Convert.ToDouble(dtgdvwFamiliares.CurrentRow.Cells["Ganho_Total_Mensal"].EditedFormattedValue.ToString()),
                                        dtgdvwFamiliares.CurrentRow.Cells["Gasto_Total_Mensal"].EditedFormattedValue.ToString() == string.Empty ? 0 : Convert.ToDouble(dtgdvwFamiliares.CurrentRow.Cells["Gasto_Total_Mensal"].EditedFormattedValue.ToString()),
                                        dtgdvwFamiliares.CurrentRow.Cells["Observacao"].EditedFormattedValue.ToString());
                }
            }
            ConsultarBDFamiliares();
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a exclusão do familiar " + strNomeAntigo, "Exclusão no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ExcluirBDFamiliares(intValorCod);
            }
            ConsultarBDFamiliares();
        }

        private void dtgdvwPrefFam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrEmpty(dtgdvwPrefFam.CurrentRow.Cells["ID"].Value.ToString()))
            {
                dtgdvwPrefFam.CurrentRow.Cells["Cod"].Selected = false;
                dtgdvwPrefFam.CurrentRow.Cells["Cod"].ReadOnly = true;
                dtgdvwPrefFam.CurrentRow.Cells["ID"].Selected = true;
                dtgdvwPrefFam.CurrentRow.Cells["ID"].ReadOnly = false;

                dtgdvwPrefFam.CurrentRow.Cells["Cod"].Value = cmbbxPrefFamFamiliar.SelectedValue;
            }
            else
            {
                intPrefFamCod = Convert.ToInt32(dtgdvwPrefFam.CurrentRow.Cells["Cod"].Value.ToString());
                strNomeAntigo = dtgdvwPrefFam.CurrentRow.Cells["Cod"].EditedFormattedValue.ToString();
                intPrefFamID = Convert.ToInt32(dtgdvwPrefFam.CurrentRow.Cells["ID"].Value.ToString());
                strValorAntigo = dtgdvwPrefFam.CurrentRow.Cells["ID"].EditedFormattedValue.ToString();
                dtgdvwPrefFam.CurrentRow.Cells["Cod"].Selected = false;
                dtgdvwPrefFam.CurrentRow.Cells["Cod"].ReadOnly = true;
                dtgdvwPrefFam.CurrentRow.Cells["ID"].Selected = false;
                dtgdvwPrefFam.CurrentRow.Cells["ID"].ReadOnly = true;
            }
        }

        private void cmbbxPrefFamFamiliar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((ComboBox)sender).Text))
            {
                ((ComboBox)sender).Text = ((ComboBox)sender).Text.Trim();
                dtgdvwPreFamRefresh();
            }
        }

        private void cmbbxPrefFamFamiliar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((ComboBox)sender).Text))
            {
                ((ComboBox)sender).Text = ((ComboBox)sender).Text.Trim();
                dtgdvwPreFamRefresh();
            }
        }

        private void bndnavbtnPrefFamPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultarBDPrefFam(Convert.ToInt32(cmbbxPrefFamFamiliar.SelectedValue.ToString()),
                                    Convert.ToInt32(bndnavcmbbxPrefFamPesquisa.Text.Substring(0, bndnavcmbbxPrefFamPesquisa.Text.IndexOf("-"))),
                                    cmbbxPrefFamFamiliar.Text,
                                    bndnavcmbbxPrefFamPesquisa.Text.Substring(bndnavcmbbxPrefFamPesquisa.Text.IndexOf("-") + 1));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Escolha da lista de preferencias do combo. NAO preencha a lista ==> " + ex.Message);
            }
            
        }

        private void bindingNavigatorAddNewItem2_Click(object sender, EventArgs e)
        {
            bolPrefFamInserido = true;
            dtgdvwPrefFam.CurrentRow.Cells["Cod"].Selected = false;
            dtgdvwPrefFam.CurrentRow.Cells["Cod"].ReadOnly = true;
            dtgdvwPrefFam.CurrentRow.Cells["ID"].Selected = true;
            dtgdvwPrefFam.CurrentRow.Cells["ID"].ReadOnly = false;
        }

        private void bndnavbtnPrefFamConfirma_Click(object sender, EventArgs e)
        {
            if (bolPrefFamInserido)
            {
                if (MessageBox.Show("Confirma a inserção da preferência " + dtgdvwPrefFam.CurrentRow.Cells["ID"].EditedFormattedValue.ToString() + " para o familiar " + cmbbxPrefFamFamiliar.Text, "Inserção no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    InserirBDPrefFam(
                        Convert.ToInt32(dtgdvwPrefFam.CurrentRow.Cells["Cod"].Value.ToString()),
                        Convert.ToInt32(dtgdvwPrefFam.CurrentRow.Cells["ID"].Value.ToString()),
                        Convert.ToSingle(dtgdvwPrefFam.CurrentRow.Cells["Intensidade"].EditedFormattedValue.ToString()),
                        dtgdvwPrefFam.CurrentRow.Cells["Cod"].EditedFormattedValue.ToString(),
                        dtgdvwPrefFam.CurrentRow.Cells["ID"].EditedFormattedValue.ToString(),
                        dtgdvwPrefFam.CurrentRow.Cells["Observacao"].EditedFormattedValue.ToString());
                }
                bolPrefFamInserido = false;
            }
            else
            {
                if (MessageBox.Show("Confirma a alteração da preferência " + dtgdvwPrefFam.CurrentRow.Cells["ID"].EditedFormattedValue.ToString() + " para o familiar " + cmbbxPrefFamFamiliar.Text, "Alteração no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    AlterarBDPrefFam(
                        Convert.ToInt32(dtgdvwPrefFam.CurrentRow.Cells["Cod"].Value.ToString()),
                        Convert.ToInt32(dtgdvwPrefFam.CurrentRow.Cells["ID"].Value.ToString()),
                        Convert.ToSingle(dtgdvwPrefFam.CurrentRow.Cells["Intensidade"].EditedFormattedValue.ToString()),
                        dtgdvwPrefFam.CurrentRow.Cells["Cod"].EditedFormattedValue.ToString(),
                        dtgdvwPrefFam.CurrentRow.Cells["ID"].EditedFormattedValue.ToString(),
                        dtgdvwPrefFam.CurrentRow.Cells["Observacao"].EditedFormattedValue.ToString());
                }

            }
            dtgdvwPreFamRefresh();
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a exclusão da preferência " + strValorAntigo + " do familiar" + cmbbxPrefFamFamiliar.Text, "Exclusão no banco de dados", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ExcluirBDPrefFam(intPrefFamCod, intPrefFamID, strValorAntigo, strNomeAntigo);
            }
            dtgdvwPreFamRefresh();
        }

        private void bndnavbtnExcellGrid_Click(object sender, EventArgs e)
        {
            AutomacaoExcelPeloGrid();
        }
        public void AutomacaoExcelPeloGrid()
        {
            //Cria automacao excel
            objExcelAplicacao = new EXCEL.Application();

            //torna visivel ou invisivel o excel
            objExcelAplicacao.Visible = true;

            //cria o arquivo excel utilizando essa automacao
            objExcelArquivo = objExcelAplicacao.Workbooks.Add();

            //associa uma planilha dentro do arquivo excel criado
            objExcelPlanilha = objExcelArquivo.Worksheets[1];
            
            //cria os objetos celula com o range a partir da planilha criada
            int intColuna = 1, intLinha = 2, intLinhaCabecalho = 1;

            objExcelPlanilhaDados = objExcelPlanilha.Cells[intLinha, intColuna];
            objExcelPlanilhaCabecalho = objExcelPlanilha.Cells[intLinhaCabecalho, intColuna];

            //Teste - atribuicao de valores para verificar o funcionamento da planilha
            //objExcelPlanilhaCabecalho.set_Value(Type.Missing, "Teste de cabecalho do excel");
            //objExcelPlanilhaDados.set_Value(Type.Missing, "Teste de dados do excel");

            //algoritmo de preenchimento da planilha excel com dois lacos, o mais externo 
            //para as linhas e o mais interno para as colunas dentro de uma linha.
            foreach (DataGridViewRow objLinhaDoGrid in dtgdvwPreferencias.Rows)
            {
                foreach (DataGridViewColumn objColunaDoGrid in dtgdvwPreferencias.Columns)
                {
                    if (intLinha <= 2)
                    {
                        objExcelPlanilhaCabecalho.set_Value(Type.Missing, objColunaDoGrid.HeaderText.ToString());
                    }

                    //atribuicao da celula de dados contendo os valores do banco de dados 
                    //que estao no grid para essa celula.
                    if(objLinhaDoGrid.Cells[intColuna -1].Value != null)
                    {
                        objExcelPlanilhaDados.set_Value(Type.Missing, objLinhaDoGrid.Cells[intColuna - 1].Value.ToString());
                    }

                    //incrementos da coluna para o objeto excel
                    intColuna++;

                    if (intLinha <= 2)
                    {
                        objExcelPlanilhaCabecalho = objExcelPlanilha.Cells[intLinhaCabecalho, intColuna];
                    }

                    //atribuicao do objPlanilhaDados para a proxima celula relativa a proxima coluna
                    objExcelPlanilhaDados = objExcelPlanilha.Cells[intLinha, intColuna];
                }

                //incrementos da linha para objeto excel
                intLinha++;
                intColuna = 1;

                //atrubuicao do objPlanilhaDados para a proxima linha na sua primeira celula
                objExcelPlanilhaDados = objExcelPlanilha.Cells[intLinha, intColuna];
            }

            //salva o arquivo excel em um arquivo nomeado
            objExcelArquivo.SaveAs(@"D:\Curso Programa\PlanilhaExcelPreferencia.xlsx",
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, EXCEL.XlSaveAsAccessMode.xlShared);

            //Fecha e destroi o excel da memoria
            objExcelAplicacao.Quit();
            MessageBox.Show("Exportação DataGridView para excel Completada", "Exportar Excel DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void bndnavbtnEnviaEmailOutlook_Click(object sender, EventArgs e)
        {
            EnviaEmailOutlook();
        }
        public void EnviaEmailOutlook()
        {
            try
            {
                objOutlookAplicacao = new EMAIL.Application();
                objOutlookItemMailMensagem = objOutlookAplicacao.CreateItem(EMAIL.OlItemType.olMailItem);
                objOutlookItemMailMensagem.SentOnBehalfOfName = "a_bsilva1@hotmail.com";
                objOutlookItemMailMensagem.To = "tonnybernardo65@gmail.com";
                objOutlookItemMailMensagem.CC = "derekdgb@gmail.com";
                objOutlookItemMailMensagem.BCC = "jornalistaantoniobernardo@gmail.com";
                objOutlookItemMailMensagem.Subject = "Teste de Envio de Email";
                objOutlookItemMailMensagem.Body = "Bom dia pessoal, " +
                    Environment.NewLine +
                    "Apenas para teste. \n" +
                    "Email automático. NÃO RESPONDA!!";

                if (MessageBox.Show("Deseja enviar anexos?", "Enviar Anexos", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ofdEmailArquivoAnexo.Title = "Escolha o(s) anexo(s) abaixo";
                    ofdEmailArquivoAnexo.InitialDirectory = @"D:\Curso Programa";
                    ofdEmailArquivoAnexo.ShowDialog();
                    if (!string.IsNullOrEmpty(ofdEmailArquivoAnexo.FileName))
                    {
                        arrayOutlookArquivosAnexos = ofdEmailArquivoAnexo.FileNames;
                    }

                    for (int i = 0; i < arrayOutlookArquivosAnexos.Length; i++)
                    {
                        objOutlookAnexoTipo = EMAIL.OlAttachmentType.olByValue;
                        lngOutlookArquivosAnexosPosicao = objOutlookItemMailMensagem.Body.Length + 1;
                        strOutLookArquivosAnexosDisplayName = arrayOutlookArquivosAnexos[i].ToString() + "Arquivo anexo teste";

                        objOutlookItemMailMensagem.Attachments.Add(
                        arrayOutlookArquivosAnexos[i],
                        objOutlookAnexoTipo,
                        lngOutlookArquivosAnexosPosicao,
                        strOutLookArquivosAnexosDisplayName);
                    }

                }
                if (MessageBox.Show("Enviar Email.com Confirmação?", "Enviar Email", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    objOutlookItemMailMensagem.Display();
                    MessageBox.Show("Mensagem enviada com sucesso", "Finaliza o envio de email", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    objOutlookItemMailMensagem.Send();
                    MessageBox.Show("Mensagem enviada com sucesso", "Finaliza o envio de email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MessageBox.Show("Finaliza o envio", "Finaliza o envio de email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
