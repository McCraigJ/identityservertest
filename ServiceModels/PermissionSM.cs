using System;
using System.Collections.Generic;

namespace ServiceModels
{
  

  public class ApplicationRoles {
    public static List<string> GetRoles()
    {
      return new List<string>() { "", "SystemAdmin" };      
    }    
  }

}
