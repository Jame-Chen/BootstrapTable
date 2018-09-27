using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{


    [MetadataType(typeof(T_PUMPTRUCKMeta))]//使用SysCommandoMetadata对SysCommando进行数据验证
    public partial class T_PUMPTRUCK
    {

        #region 自定义属性，即由数据实体扩展的实体
        public string N_ISSHState { get; set; }//审核状态
        [Display(Name = "抢险状态")]
        public string QXZT { get; set; }//抢先状态
        [Display(Name = "突击队")]
        public string S_SysCommandoName { get; set; }//突击队
        [Display(Name = "所属区")]
        public string S_QXName { get; set; }//所属区名称

        [Display(Name = "视频编号")]

        public string S_SBBH { get; set; }//

        public string markOld { get; set; }
        public string userID { get; set; }
        [Display(Name = "编号")]
        public string S_PT_ID2 { get; set; }

        [Display(Name = "停放坐标")]
        public string S_XY { get; set; }

        #endregion

    }
    public class T_PUMPTRUCKMeta
    {
        [Required]
        [Display(Name = "编号")]
        // [Remote("CheckS_PT_ID", "BCGL", ErrorMessage = "该编号已存在", AdditionalFields = "S_PT_ID2")]
        public string S_PT_ID { get; set; }
        [Display(Name = "突击队")]
        [Required]
        public string S_SYSCOMMANDOID { get; set; }
        [Display(Name = "车辆名称")]
        public string S_PT_NAME { get; set; }

        //[ScaffoldColumn(true)]
        [Display(Name = "所属区")]

        [Required]
        public string S_QX { get; set; }

        //[ScaffoldColumn(true)]
        [Display(Name = "车牌号")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
        [Required]
        public string S_PZ { get; set; }

        [RegularExpression("^[0-9]{2,14}$", ErrorMessage = "请输入数字")]
        //[ScaffoldColumn(true)]
        [Display(Name = "泵车流量(m³/h)")]
        //      [StringLength(10, ErrorMessage = "长度不可超过10")]
        public Nullable<decimal> S_PT_FLOW { get; set; }

        [RegularExpression("^[0-9]{2,14}$", ErrorMessage = "请输入数字")]
        //[ScaffoldColumn(true)]
        [Display(Name = "泵车扬程(m)")]
        [Required]
        //   [StringLength(10, ErrorMessage = "长度不可超过10")]
        public Nullable<decimal> S_PT_LIFT { get; set; }



        //[ScaffoldColumn(true)]
        [Display(Name = "停放地址")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string S_ADD { get; set; }

        //[ScaffoldColumn(true)]
        [Display(Name = "泵车联系人")]
        [StringLength(10, ErrorMessage = "长度不可超过10")]
        [Required]
        public string S_PT_CONTACTS { get; set; }

        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        [Display(Name = "联系电话")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]

        [Required]
        public string S_PT_CONTACTPHONE { get; set; }

        //[ScaffoldColumn(true)]
        [Display(Name = "保障范围")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string S_PT_AREA { get; set; }

        [Display(Name = "分类")]
        [Required]
        public string N_PT_CLASS { get; set; }



        //[ScaffoldColumn(true)]
        [Display(Name = "备注")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public string S_BZ { get; set; }

        [Display(Name = "视频编号")]
        //  [Remote("CheckS_SBBH", "BCGL", ErrorMessage = "该编号已存在", AdditionalFields = "S_PT_ID2")]
        public string S_SPBH { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
