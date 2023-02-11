using ScriptExecutorPrime.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TsSolution.Security.Crypto;

namespace ScriptExecutorPrime.View
{
    /// <summary>
    /// Interaction logic for SecurityTestView.xaml
    /// </summary>
    public partial class SecurityTestView : UserControl
    {
        private SecurityTestViewModel VM;

        public SecurityTestView()
        {
            InitializeComponent();
            VM = new SecurityTestViewModel();
            DataContext = VM;
        }

        private void ButtonEncrypt_Click(object sender, RoutedEventArgs e)
        {
            var textToEncrypt = TextBoxEncrypt.Text;
            var encryptedText = TextAESEncrypter.EncryptData(textToEncrypt);
            TextBoxOutputEncrypt.Text = encryptedText;
        }

        private void ButtonDecrypt_Click(object sender, RoutedEventArgs e)
        {
            var textToDecrypt = TextBoxDecrypt.Text;
            var decryptedText = TextAESEncrypter.DecryptData(textToDecrypt);
            TextBoxOutputDecrypt.Text = decryptedText;
        }
    }
}