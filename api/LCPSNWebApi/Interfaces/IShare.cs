﻿using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IShare
    {
        Task<ActionResult<IEnumerable<Share>>> GetShares();
        Task<ActionResult<IEnumerable<Share>>> GetSharesById(int? id);
        IActionResult GetSharesAsEnumList();
        Task<IActionResult> PutShares(int? id, Share Shares);
        Task<ActionResult<IEnumerable<Share>>> PostShares(Share SharesData);
        Task<IActionResult> DeleteShares(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}