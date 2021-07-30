using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Business.Interfaces;
using MISA.CukCuk.Common.Const;
using MISA.CukCuk.Common.Entity;
using MISA.CukCuk.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        #region Constructor
        IBaseServices<MISAEntity> _baseServices;
        public BaseEntityController(IBaseServices<MISAEntity> baseServices)
        {
            _baseServices = baseServices;
        }
        #endregion

        #region Methods

        #region Lấy toàn bộ các bản ghi trong database
        /// <summary>
        /// Lấy toàn bộ bản ghi trong database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var entities = _baseServices.GetAll();
                if (entities.Count() > 0)
                {
                    return Ok(entities);
                }
                else return NoContent();
            }
            catch (Exception ex)
            {
                ResponseMessage resMsg = new ResponseMessage();
                resMsg.DevMsg = ex.Message;
                resMsg.UserMsg = Resources.Server_Error;
                resMsg.ErrorCode = MISAConst.MISAErrorException;
                return StatusCode(500, resMsg);
            }
        }
        #endregion

        #region Lấy bản ghi theo khóa chính
        /// <summary>
        /// Lấy bản ghi theo khóa chính
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var entities = _baseServices.GetById(id);
                if (entities != null)
                {
                    return Ok(entities);
                }
                else return NoContent();
            }
            catch (Exception ex)
            {
                ResponseMessage resMsg = new ResponseMessage();
                resMsg.DevMsg = ex.Message;
                resMsg.UserMsg = Resources.Server_Error;
                resMsg.ErrorCode = MISAConst.MISAErrorException;
                return StatusCode(500, resMsg);
            }
        }
        #endregion

        #region Thêm mới bản ghi
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {
            try
            {
                var resMsg = _baseServices.AddNewEntity(entity);
                if (resMsg.Success)
                    return Ok(resMsg);
                else return StatusCode(400, resMsg);
            }
            catch (Exception ex)
            {
                ResponseMessage resMsg = new ResponseMessage();
                resMsg.DevMsg = ex.Message;
                resMsg.UserMsg = Resources.Server_Error;
                resMsg.ErrorCode = MISAConst.MISAErrorException;
                return StatusCode(500, resMsg);
            }
        }
        #endregion

        #region Xóa bản ghi theo khóa chính
        /// <summary>
        /// Xóa bản ghi theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var resMsg = _baseServices.DeleteById(id);
                if (resMsg.Success)
                    return Ok(resMsg);
                else return StatusCode(400, resMsg);
            }
            catch (Exception ex)
            {
                ResponseMessage resMsg = new ResponseMessage();
                resMsg.DevMsg = ex.Message;
                resMsg.UserMsg = Resources.Server_Error;
                resMsg.ErrorCode = MISAConst.MISAErrorException;
                return StatusCode(500, resMsg);
            }
        }
        #endregion

        #region Cập nhật bản ghi
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi đã được sửa đổi</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(MISAEntity entity)
        {
            try
            {
                var resMsg = _baseServices.UpdateEntity(entity);
                if (resMsg.Success)
                    return Ok(resMsg);
                else return StatusCode(400, resMsg);
            }
            catch (Exception ex)
            {
                ResponseMessage resMsg = new ResponseMessage();
                resMsg.DevMsg = ex.Message;
                resMsg.UserMsg = Resources.Server_Error;
                resMsg.ErrorCode = MISAConst.MISAErrorException;
                return StatusCode(500, resMsg);
            }
        }
        #endregion
        #endregion
    }
}
