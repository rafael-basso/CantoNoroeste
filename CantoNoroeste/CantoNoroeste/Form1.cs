using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CantoNoroeste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Multiline = true;
            textBox4.Multiline = true;
            textBox5.Multiline = true;
                        

            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrWhiteSpace(textBox2.Text) &&
               !string.IsNullOrWhiteSpace(textBox3.Text) &&
               !string.IsNullOrWhiteSpace(textBox4.Text) &&
               !string.IsNullOrWhiteSpace(textBox5.Text)) 
               
            {
                int oferta = Convert.ToInt16(textBox1.Text); //linha
                int demanda = Convert.ToInt16(textBox2.Text); //coluna

                int cantoLinha = 0;
                int cantoColuna = 0;
                int[,] temp = new int[oferta, demanda];
                int[,] res = new int[oferta, demanda];

                //PARA PEGAR VALORES DE CUSTO
                string[] str = textBox3.Text.Split('\r');
                int[] num = new int[oferta * demanda];
                int i, j;
                if (str.Length != oferta * demanda)
                {
                    MessageBox.Show("Quantidade de valores para CUSTO fora do tamanho\n" +
                                    "LEMBRETE: apos ultimo numero NAO pressionar ENTER ou ESPACO");
                    //textBox3.Text = "";
                    return;
                }
                for (i = 0; i < oferta * demanda; i++)
                {
                    str[i] = str[i].Replace("\n", "");
                    num[i] = int.Parse(str[i]); //transforma string em int
                    //textBox6.Text += $"{num[i] + " "}\r\n";
                }
                int[,] custo = new int[oferta, demanda]; //criacao de matriz
                int k = 0;
                for (i = 0; i < oferta; i++) //passar os valores de num[] para custo[][]
                {
                    for (j = 0; j < demanda; j++, k++)
                    {
                        custo[i, j] = num[k];
                    }
                }
                
                //PARA PEGAR VALORES DE somaO
                int[] somaO = new int[oferta];
                string[] strO = textBox4.Text.Split('\r');
                if (strO.Length != oferta)
                {
                    MessageBox.Show("Quantidade de valores para DISPONIBILIDADE fora do tamanho\n" +
                                    "LEMBRETE: apos ultimo numero NAO pressionar ENTER ou ESPACO");
                    //textBox4.Text = "";
                    return;
                }
                for (i = 0; i < oferta; i++)
                {
                    strO[i] = strO[i].Replace("\n", "");
                    somaO[i] = int.Parse(strO[i]); //transforma string em int
                }
                
                //PARA PEGAR VALORES DE somaD
                int[] somaD = new int[demanda];
                string[] strD = textBox5.Text.Split('\r');
                if (strD.Length != demanda)
                {
                    MessageBox.Show("Quantidade de valores para NECESSIDADE fora do tamanho\n" +
                                    "LEMBRETE: apos ultimo numero NAO pressionar ENTER ou ESPACO");
                    //textBox5.Text = "";
                    return;
                }
                for (i = 0; i < demanda; i++)
                {
                    strD[i] = strD[i].Replace("\n", "");
                    somaD[i] = int.Parse(strD[i]); //transforma string em int
                }

                int soma = 0, somaOferta = 0, somaDemanda = 0;
                for (i = 0; i < oferta; i++)
                {
                    somaOferta = somaOferta + somaO[i];
                    //textBox7.Text += $"{somaOferta + " "}";
                }

                for (i = 0; i < demanda; i++)
                {
                    somaDemanda = somaDemanda + somaD[i];
                    //textBox7.Text += $"{somaDemanda + " "}";
                }

                if (somaOferta != somaDemanda)
                {
                    MessageBox.Show("Soma da disponibilidade e necessidade NAO estao balanceadas");
                    return;
                }

                //CALCULAR CANTO NOROESTE
                for (i = 1; i < oferta + demanda; i++)
                {
                    if (somaO[cantoLinha] <= somaD[cantoColuna])
                    {
                        temp[cantoLinha, cantoColuna] = somaO[cantoLinha];
                        for (j = cantoColuna + 1; j < demanda; j++)
                        {
                            temp[cantoLinha, j] = 0;
                        }
                        somaD[cantoColuna] = somaD[cantoColuna] - somaO[cantoLinha];
                        cantoLinha++;
                    }
                    else
                    {
                        temp[cantoLinha, cantoColuna] = somaD[cantoColuna];
                        for (j = cantoLinha + 1; j < oferta; j++)
                        {
                            temp[j, cantoColuna] = 0;
                        }
                        somaO[cantoLinha] = somaO[cantoLinha] - somaD[cantoColuna];
                        cantoColuna++;
                    }
                }

                
                for (i = 0; i < oferta; i++)
                        for (j = 0; j < demanda; j++)
                            res[i, j] = custo[i, j] * temp[i, j];

                    for (i = 0; i < oferta; i++)
                        for (j = 0; j < demanda; j++)
                            soma = soma + res[i, j];

                    for (i = 0; i < oferta; i++)
                    {
                        for (j = 0; j < demanda; j++)
                            textBox6.Text += $"{temp[i, j] + " "}";
                        textBox6.Text += $"\r\n";
                    }

                    textBox7.Text = soma.ToString();
            }
            else
            {
                MessageBox.Show("Falta preencher valores");
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                int oferta = Convert.ToInt32(textBox1.Text); //linha
                int demanda = Convert.ToInt32(textBox2.Text); //coluna
                if (oferta > 10 || demanda > 10)
                {
                    MessageBox.Show("Valor maximo para oferta ou demanda = 10");
                    return;
                }
                else
                {
                    textBox3.Height = (oferta * demanda) * 20;
                    textBox4.Height = oferta * 30;
                    textBox5.Height = demanda * 30;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                }   
            }
            else
            {
                MessageBox.Show("Falta preencher valores");
                return;
            }

        }

        //KeyPress event -> no tools do textbox, aceita so numero e backspace ou enter para o textbox multiline
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 08) // 08 = backspace
                e.Handled = true;
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 08) 
                e.Handled = true;
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 13) // 13 = enter
                e.Handled = true;
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 13) 
                e.Handled = true;
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 13) 
                e.Handled = true;
        }



        //MISDOUBLE-CLICK
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        
    }
}
