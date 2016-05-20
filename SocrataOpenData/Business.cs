﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocrataOpenData
{
  [Serializable]
  class Business
  {
    public string BusinessId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
  }
 
}
