using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FM2E.Model.Examine;
using System.Collections;
using FM2E.IDAL.Examine;
using FM2E.IDAL.Utils;
using System.Data.Common;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

namespace FM2E.BLL.Examine
{

    /// <summary>
    /// 考核业务逻辑类
    /// </summary>
    public class Examine
    {
        private IExamine dal = FM2E.DALFactory.ExamineAccess.CreateExamine();

        #region 考核项
        /// <summary>
        /// 保存考核项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long SaveExamineItem(ExamineItemInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                if (model.ExamItemID == 0)
                    id=dal.AddExamineItem(model,trans);
                else id=dal.UpdateExamineItem(model,trans);

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("保存考核项失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }
        /// <summary>
        /// 删除考核项
        /// </summary>
        /// <param name="id"></param>
        public void DeleteExamineItem(long id)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            
            try
            {
                trans = transDAL.GetTransaction();

                dal.DeleteExamineItem(id, trans);

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("删除考核项失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            
        }
        /// <summary>
        /// 获取某考核项的所有子考核项（树结构）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetSubExamineItems(long id)
        {
            return dal.GetSubExamineItems(id);
        }
        /// <summary>
        /// 获取某考核项的所有直接子考核项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetChildExamineItems(long id)
        {

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;

            IList list = new ArrayList();
            try
            {
                trans = transDAL.GetTransaction();

                list= dal.GetChildExamineItems(id,trans);

                trans.Commit();
            }
            catch (Exception ex)
            {
                list.Clear();
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("获取子考核项失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return list;
        }
        /// <summary>
        /// 获取某个特定的考核项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineItemInfo GetExamineItem(long id)
        {
            return dal.GetExamineItem(id);
        }
        
        #endregion

        #region 考核表
        /// <summary>
        /// 保存考核表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long SaveExamine(ExamineInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                if (model.ExamSheetID == 0)
                    id = dal.AddExamine(model, trans);
                else id = dal.UpdateExamine(model, trans);

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("保存考核表失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }
        /// <summary>
        /// 删除考核表
        /// </summary>
        /// <param name="model"></param>
        public void DeleteExamine(long id)
        {
            dal.DeleteExamine(id);
        }
        /// <summary>
        /// 考核表确认 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmer"></param>
        /// <param name="result"></param>
        /// <param name="remark"></param>
        /// <param name="confirmDate"></param>
        public void ExamineConfirm(long id, string confirmer, ExamineConfirmResult result, string remark, DateTime confirmDate,ExamineSheetStatus status)
        {
            dal.ExamineConfirm(id, confirmer, result, remark, confirmDate,status);
        }
        /// <summary>
        /// 获取考核表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineInfo GetExamine(long id)
        {
            return dal.GetExamine(id);
        }
        /// <summary>
        /// 查询考核表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetExamines(ExamineSearchInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.GetExamines(qp, out recordCount);
        }
        /// <summary>
        /// 传入一个考核明细列表，得出各层的得分(日常考核)
        /// </summary>
        /// <param name="examineDetail"></param>
        public void ComputeDailyExamineScore(ExamineDetailInfo root,IList examineDetail)
        {
            //examineDetail中第一个结点为根结点
            if (root == null)
                return;
            IList childList = GetExamineChilds(root.ExamItemID, examineDetail);
            if (root.CanAddChild)
            {
                root.ExamScore = 100;
                if ((Math.Abs(root.Score / 100 - 1) <= 0.000001))
                {
                    root.ExamScore = 0;
                }
                //如果是最低层次的考核项，则其子结点都为扣分项
                foreach (ExamineDetailInfo item in childList)
                {
                    root.ExamScore -= item.Deduct;
                }
                if (root.ExamScore <= root.Threshold)
                    root.ExamScore = 0;
            }
            else if (!root.CanAddChild && root.ChildCount != 0)
            {
                //非最低层次的考核项
                root.ExamScore = 0;
                foreach (ExamineDetailInfo item in childList)
                {
                    ComputeDailyExamineScore(item, examineDetail);   //递归计算得分
                    //if ((Math.Abs(item.Score/100 - 1) <= 0.000001))
                    //{
                    //    //如果item.Score等于1
                    //    root.ExamScore = root.ExamScore - (100 - item.ExamScore);
                    //}else
                    root.ExamScore += (item.Score / 100) * item.ExamScore;
                }
                if (root.ExamScore <= root.Threshold)
                    root.ExamScore = 0;
            }
        }
        /// <summary>
        /// 传入一个考核明细列表，得出各层的得分(季度考核)
        /// </summary>
        /// <param name="examineDetail"></param>
        public void ComputeSeasonExamineScore(ExamineDetailInfo root, IList examineDetail)
        {
            //examineDetail中第一个结点为根结点
            if (root == null)
                return;
            IList childList = GetExamineChilds(root.ExamItemID, examineDetail);
            if (root.CanAddChild)
            {
                //如果是最低层次的考核项，则其子结点都为扣分项
                root.ExamScore = root.Score;
                foreach (ExamineDetailInfo item in childList)
                {
                    root.ExamScore -= item.Deduct;
                }
                if (root.ExamScore <= root.Threshold)
                    root.ExamScore = 0;
            }
            else if (!root.CanAddChild && root.ChildCount != 0)
            {
                //非最低层次的考核项
                root.ExamScore = 0;
                foreach (ExamineDetailInfo item in childList)
                {
                    ComputeSeasonExamineScore(item, examineDetail);   //递归计算得分
                    root.ExamScore += item.ExamScore;
                }
                if (root.ExamScore <= root.Threshold)
                    root.ExamScore = 0;
            }
        }
        /// <summary>
        /// 获取考核子结点
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="examineDetail"></param>
        /// <returns></returns>
        private IList GetExamineChilds(long parentID,IList examineDetail)
        {
            ArrayList list = new ArrayList();
            foreach (ExamineDetailInfo item in examineDetail)
            {
                if (item.ParentItem == parentID)
                {
                    list.Add(item);
                }
            }
            return list;
        }
          /// <summary>
        /// 获取符合条件的所有考核表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetExamines(ExamineSearchInfo term)
        {
            return dal.GetExamines(term);
        }
        #endregion

        #region 考核结果
        /// <summary>
        /// 保存考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long SaveExamineResult(ExamineResultInfo model)
        {
            if (model.ID == 0)
            {
                return dal.AddExamineResult(model);
            }
            else
            {
                return dal.UpdateExamineResult(model);
            }
        }
        /// <summary>
        /// 根据ID获取已保存的考核结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineResultInfo GetExamineResult(long id)
        {
            return dal.GetExamineResult(id);
        }
        /// <summary>
        /// 删除考核结果
        /// </summary>
        /// <param name="id"></param>
        public void DeleteExamineResult(long id)
        {
            dal.DeleteExamineResult(id);
        }
        /// <summary>
        /// 根据查询条件获取考核结果表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetExamineResults(ExamineResultSearchInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.GetExamineResults(qp, out recordCount);
        }
        #endregion
    }
}
