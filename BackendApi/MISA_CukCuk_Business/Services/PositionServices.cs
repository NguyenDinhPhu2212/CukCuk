using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Const;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Core.Services
{
    public class PositionServices : BaseServices<Position>, IPositionServices
    {
        #region Constructor
        IPositionRepository _positionRepository;
        public PositionServices(IPositionRepository positionRepository) : base(positionRepository)
        {
            _positionRepository = positionRepository;
        }
        #endregion

    }
}
