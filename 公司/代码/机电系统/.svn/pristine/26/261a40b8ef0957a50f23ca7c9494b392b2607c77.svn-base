using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Examine;
using System.Collections;
using System.Data.Common;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Examine
{
    /// <summary>
    /// 考核数据访问接口
    /// </summary>
    public interface IExamine
    {
        #region 考核项
        /// <summary>
        /// 添加考核项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long AddExamineItem(ExamineItemInfo model,DbTransaction trans);
        /// <summary>
        /// 修改考核项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long UpdateExamineItem(ExamineItemInfo model,DbTransaction trans);
        /// <summary>
        /// 删除考核项
        /// </summary>
        /// <param name="id"></param>
        void DeleteExamineItem(long id,DbTransaction trans);
        /// <summary>
        /// 获取某考核项的子考核项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSubExamineItems(long id);
        IList GetChildExamineItems(long id, DbTransaction trans);
           /// <summary>
        /// 获取某个特定的考核项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineItemInfo GetExamineItem(long id);
        #endregion

        #region 考核表
        /// <summary>
        /// 添加考核表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long AddExamine(ExamineInfo model, DbTransaction trans);
        /// <summary>
        ///更新考核表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long UpdateExamine(ExamineInfo model, DbTransaction trans);
        /// <summary>
        /// 删除考核表
        /// </summary>
        /// <param name="id"></param>
        void DeleteExamine(long id);
        /// <summary>
        /// 获取考核表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineInfo GetExamine(long id);
        /// <summary>
        /// 获取考核表查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GetSearchTerm(ExamineSearchInfo term);
        /// <summary>
        /// 查询考核表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetExamines(QueryParam term, out int recordCount);
        /// <summary>
        /// 获取符合条件的所有考核表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetExamines(ExamineSearchInfo term);
        /// <summary>
        /// 考核表确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmer"></param>
        /// <param name="result"></param>
        /// <param name="remark"></param>
        /// <param name="confirmDate"></param>
        void ExamineConfirm(long id, string confirmer, ExamineConfirmResult result, string remark, DateTime confirmDate,ExamineSheetStatus status);
        #endregion

        #region 考核结果
        /// <summary>
        /// 添加考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long AddExamineResult(ExamineResultInfo model);
        /// <summary>
        /// 修改考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long UpdateExamineResult(ExamineResultInfo model);
        /// <summary>
        /// 获取已保存的考核结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineResultInfo GetExamineResult(long id);
        /// <summary>
        /// 删除考核结果
        /// </summary>
        /// <param name="id"></param>
        void DeleteExamineResult(long id);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GetSearchTerm(ExamineResultSearchInfo term);
        /// <summary>
        /// 根据查询条件获取考核结果表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetExamineResults(QueryParam term, out int recordCount);
        #endregion
    }
}
