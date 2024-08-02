using System.Diagnostics;
using System.Security.Principal;
//using static System.Net.Mime.MediaTypeNames;

namespace adminlib
{
    public class CWAdminLib
    {
        public bool Admin = false;
        public bool IsAdmin() {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            Admin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            return Admin;
        }
        public void RestartAsAdmin(string FileName) {
            if (Admin == false) {
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = FileName,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(processInfo);
            };
        }
    }
}
