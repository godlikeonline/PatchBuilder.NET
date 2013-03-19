using System.ServiceProcess;

namespace PatchDeployer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[] 
                                              { 
                                                  new PatchDeployer() 
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
