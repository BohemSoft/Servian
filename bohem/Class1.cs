using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.Win32;
using System.Security.Principal;


namespace bohem
{
    public class Class1
    {
        public bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public void hizmetCalistir(string hizmetadi)
        {
            ServiceController servis = new ServiceController(hizmetadi);
            servis.Start();
        }

        public void hizmetDurdur(string hizmetadi)
        {
            ServiceController servis = new ServiceController(hizmetadi);
            servis.Stop();
        }
    }
}
