using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Interfaces;
using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Repository
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        #region Constructor
        public PositionRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

    }
}
