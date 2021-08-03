using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class PositionController : BaseEntityController<Position>
    {
        #region Constructor
        IPositionServices _positionServices;
        public PositionController(IPositionServices positionServices) : base(positionServices)
        {
            _positionServices = positionServices;
        }
        #endregion
    }
}
