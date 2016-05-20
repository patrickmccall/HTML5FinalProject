using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocrataOpenData
{
  [Serializable]
  class Inspection
  {
    public int Score { get; set; }
    public string Result { get; set; }
  }

  enum InspectionResult
  {
    Unsatisfactory
      , Complete
      ,Satisfactory
      ,NotAccessible

}
}
