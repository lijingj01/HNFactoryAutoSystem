//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : hnfactoryautodb
	/// Data Source    : WinMySqlServer
	/// Server Version : 8.0.18
	/// </summary>
	public partial class HnfactoryautodbDB : LinqToDB.Data.DataConnection
	{
		public ITable<BDeviceset>            BDevicesets            { get { return this.GetTable<BDeviceset>(); } }
		/// <summary>
		/// 设备生产线关联表
		/// </summary>
		public ITable<FAssemblylinedevice>   FAssemblylinedevices   { get { return this.GetTable<FAssemblylinedevice>(); } }
		/// <summary>
		/// 工厂生产线表
		/// </summary>
		public ITable<FAssemblylineinfo>     FAssemblylineinfoes    { get { return this.GetTable<FAssemblylineinfo>(); } }
		public ITable<FDeviceinfo>           FDeviceinfoes          { get { return this.GetTable<FDeviceinfo>(); } }
		public ITable<FFactoryinfo>          FFactoryinfoes         { get { return this.GetTable<FFactoryinfo>(); } }
		public ITable<FSensorinfo>           FSensorinfoes          { get { return this.GetTable<FSensorinfo>(); } }
		/// <summary>
		/// 设备指令操作日志表
		/// </summary>
		public ITable<LDeviceactionlog>      LDeviceactionlogs      { get { return this.GetTable<LDeviceactionlog>(); } }
		/// <summary>
		/// 设备生产日志表
		/// </summary>
		public ITable<LDeviceproducelog>     LDeviceproducelogs     { get { return this.GetTable<LDeviceproducelog>(); } }
		/// <summary>
		/// 设备生产日志参数表
		/// </summary>
		public ITable<LDeviceproducelogpar>  LDeviceproducelogpars  { get { return this.GetTable<LDeviceproducelogpar>(); } }
		/// <summary>
		/// 工艺子流程执行日志表
		/// </summary>
		public ITable<LExprocesslog>         LExprocesslogs         { get { return this.GetTable<LExprocesslog>(); } }
		/// <summary>
		/// 工艺子流程步骤日志表
		/// </summary>
		public ITable<LExprocesssteplog>     LExprocesssteplogs     { get { return this.GetTable<LExprocesssteplog>(); } }
		/// <summary>
		/// 工艺子流程步骤参数日志表
		/// </summary>
		public ITable<LExprocessstepparslog> LExprocessstepparslogs { get { return this.GetTable<LExprocessstepparslog>(); } }
		/// <summary>
		/// 工艺流程执行日志主表
		/// </summary>
		public ITable<LProcesslog>           LProcesslogs           { get { return this.GetTable<LProcesslog>(); } }
		public ITable<PExprocess>            PExprocesses           { get { return this.GetTable<PExprocess>(); } }
		public ITable<PExprocessstep>        PExprocesssteps        { get { return this.GetTable<PExprocessstep>(); } }
		public ITable<PExprocesssteppar>     PExprocesssteppars     { get { return this.GetTable<PExprocesssteppar>(); } }
		public ITable<PMainprocess>          PMainprocesses         { get { return this.GetTable<PMainprocess>(); } }
		/// <summary>
		/// VIEW
		/// </summary>
		public ITable<VAssemblylinedevice>   VAssemblylinedevices   { get { return this.GetTable<VAssemblylinedevice>(); } }

		public HnfactoryautodbDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public HnfactoryautodbDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table("b_deviceset")]
	public partial class BDeviceset
	{
		[Column("id"), PrimaryKey,  Identity] public int    Id                { get; set; } // int(11)
		[Column(),     NotNull              ] public string ProcessDeviceId   { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string ProcessDeviceName { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string DeviceType        { get; set; } // varchar(10)
		[Column(),        Nullable          ] public string MaterialType      { get; set; } // varchar(10)
		[Column(),        Nullable          ] public string Parameter1        { get; set; } // varchar(45)
		[Column(),        Nullable          ] public string Parameter2        { get; set; } // varchar(45)
		[Column(),        Nullable          ] public string Parameter3        { get; set; } // varchar(45)
	}

	/// <summary>
	/// 设备生产线关联表
	/// </summary>
	[Table("f_assemblylinedevices")]
	public partial class FAssemblylinedevice
	{
		[Column("id"), PrimaryKey, Identity] public int    Id             { get; set; } // int(11)
		[Column(),     NotNull             ] public string DeviceId       { get; set; } // varchar(100)
		[Column(),     NotNull             ] public string AssemblyLineId { get; set; } // varchar(100)
	}

	/// <summary>
	/// 工厂生产线表
	/// </summary>
	[Table("f_assemblylineinfo")]
	public partial class FAssemblylineinfo
	{
		[Column("id"), PrimaryKey,  Identity] public int    Id                { get; set; } // int(11)
		[Column(),     NotNull              ] public string AssemblyLineId    { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string ProcessId         { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string FactoryId         { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string AssemblyLineTitle { get; set; } // varchar(200)
		[Column(),        Nullable          ] public string Parameter1        { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string Parameter2        { get; set; } // varchar(100)
	}

	[Table("f_deviceinfo")]
	public partial class FDeviceinfo
	{
		[Column("id"), PrimaryKey,  Identity] public int    Id              { get; set; } // int(11)
		[Column(),     NotNull              ] public string DeviceId        { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string DeviceName      { get; set; } // varchar(200)
		[Column(),        Nullable          ] public string DeviceType      { get; set; } // varchar(5)
		[Column(),        Nullable          ] public string ProcessDeviceId { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string DeviceStatus    { get; set; } // varchar(5)
		[Column(),        Nullable          ] public string Parameter1      { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string Parameter2      { get; set; } // varchar(100)
	}

	[Table("f_factoryinfo")]
	public partial class FFactoryinfo
	{
		[Column("id"), PrimaryKey,  Identity] public int    Id           { get; set; } // int(11)
		[Column(),     NotNull              ] public string FactoryId    { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string FactoryTitle { get; set; } // varchar(200)
		[Column(),        Nullable          ] public string City         { get; set; } // varchar(45)
		[Column(),        Nullable          ] public string Parameter1   { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string Parameter2   { get; set; } // varchar(100)
	}

	[Table("f_sensorinfo")]
	public partial class FSensorinfo
	{
		[Column("sid"),       PrimaryKey,  Identity] public int      Sid          { get; set; } // int(11)
		[Column(),            NotNull              ] public string   SensorId     { get; set; } // varchar(100)
		[Column(),               Nullable          ] public string   SensorName   { get; set; } // varchar(200)
		[Column(),               Nullable          ] public string   Power        { get; set; } // varchar(30)
		[Column(),               Nullable          ] public string   Voltage      { get; set; } // varchar(30)
		[Column(),               Nullable          ] public string   StarterType  { get; set; } // varchar(30)
		[Column("IO_DI"),        Nullable          ] public int?     IoDi         { get; set; } // int(11)
		[Column("IO_DO"),        Nullable          ] public int?     IoDo         { get; set; } // int(11)
		[Column("IO_AI"),        Nullable          ] public int?     IoAi         { get; set; } // int(11)
		[Column("IO_AO"),        Nullable          ] public int?     IoAo         { get; set; } // int(11)
		[Column("IO_Modbus"),    Nullable          ] public int?     IoModbus     { get; set; } // int(11)
		[Column(),               Nullable          ] public decimal? MinEU        { get; set; } // decimal(10,0)
		[Column(),               Nullable          ] public decimal? MaxEu        { get; set; } // decimal(10,0)
		[Column(),               Nullable          ] public string   Units        { get; set; } // varchar(30)
		[Column(),               Nullable          ] public string   SComment     { get; set; } // varchar(200)
		[Column(),            NotNull              ] public int      SensorStatus { get; set; } // int(11)
		[Column("PLC_Id"),       Nullable          ] public string   PlcId        { get; set; } // varchar(100)
		[Column(),               Nullable          ] public string   DeviceId     { get; set; } // varchar(100)
		/// <summary>
		/// 连接到的设备编号
		/// </summary>
		[Column(),               Nullable          ] public string   ToDeviceId   { get; set; } // varchar(100)
	}

	/// <summary>
	/// 设备指令操作日志表
	/// </summary>
	[Table("l_deviceactionlog")]
	public partial class LDeviceactionlog
	{
		[Column("id"), PrimaryKey,  Identity] public int      Id                { get; set; } // int(11)
		/// <summary>
		/// 日志流水号
		/// </summary>
		[Column(),     NotNull              ] public string   DeviceActionLogId { get; set; } // varchar(100)
		/// <summary>
		/// 设备编号
		/// </summary>
		[Column(),     NotNull              ] public string   DeviceId          { get; set; } // varchar(100)
		/// <summary>
		/// 操作记录时间
		/// </summary>
		[Column(),     NotNull              ] public DateTime Created           { get; set; } // datetime(6)
		/// <summary>
		/// 操作的人或设备编号
		/// </summary>
		[Column(),        Nullable          ] public string   CreateUser        { get; set; } // varchar(30)
		/// <summary>
		/// 控制传感器编号
		/// </summary>
		[Column(),        Nullable          ] public string   SensorId          { get; set; } // varchar(100)
		/// <summary>
		/// 操作类型
		/// </summary>
		[Column(),        Nullable          ] public string   ActionType        { get; set; } // varchar(5)
		/// <summary>
		/// 参数类型
		/// </summary>
		[Column(),        Nullable          ] public string   ParType           { get; set; } // varchar(5)
		/// <summary>
		/// 参数单位
		/// </summary>
		[Column(),        Nullable          ] public string   ParUnit           { get; set; } // varchar(5)
		[Column(),     NotNull              ] public decimal  ParValue          { get; set; } // decimal(10,0)
		/// <summary>
		/// 操作目标的设备编号
		/// </summary>
		[Column(),        Nullable          ] public string   ToDeviceId        { get; set; } // varchar(100)
		/// <summary>
		/// 目标的传感器编号
		/// </summary>
		[Column(),        Nullable          ] public string   ToSensorId        { get; set; } // varchar(100)
	}

	/// <summary>
	/// 设备生产日志表
	/// </summary>
	[Table("l_deviceproducelog")]
	public partial class LDeviceproducelog
	{
		[Column("id"), PrimaryKey,  Identity] public int       Id                 { get; set; } // int(11)
		/// <summary>
		/// 日志流水编号{设备编号}-{年月日时分秒}
		/// 
		/// </summary>
		[Column(),     NotNull              ] public string    DeviceProduceLogId { get; set; } // varchar(100)
		/// <summary>
		/// 设备唯一编号
		/// </summary>
		[Column(),     NotNull              ] public string    DeviceId           { get; set; } // varchar(100)
		/// <summary>
		/// 记录时间
		/// </summary>
		[Column(),        Nullable          ] public DateTime? Created            { get; set; } // datetime(6)
		/// <summary>
		/// 设备当前状态
		/// </summary>
		[Column(),        Nullable          ] public string    DeviceStatus       { get; set; } // varchar(5)
		/// <summary>
		/// 采集来源传感器编号
		/// </summary>
		[Column(),        Nullable          ] public string    SensorId           { get; set; } // varchar(100)
		/// <summary>
		/// 传感器当前状态
		/// </summary>
		[Column(),        Nullable          ] public string    SensorStatus       { get; set; } // varchar(5)
		/// <summary>
		/// 参数类型
		/// </summary>
		[Column(),        Nullable          ] public string    ParType            { get; set; } // varchar(5)
		/// <summary>
		/// 参数单位
		/// </summary>
		[Column(),        Nullable          ] public string    ParUnit            { get; set; } // varchar(5)
		/// <summary>
		/// 参数数值
		/// </summary>
		[Column(),        Nullable          ] public decimal?  ParValue           { get; set; } // decimal(10,0)
	}

	/// <summary>
	/// 设备生产日志参数表
	/// </summary>
	[Table("l_deviceproducelogpar")]
	public partial class LDeviceproducelogpar
	{
		[Column("id"), PrimaryKey,  Identity] public int       Id                 { get; set; } // int(11)
		/// <summary>
		/// 流水编号{设备编号}-{参数类型}-{年月日时分秒}
		/// </summary>
		[Column(),     NotNull              ] public string    LogParId           { get; set; } // varchar(100)
		/// <summary>
		/// 设备日志编号
		/// </summary>
		[Column(),     NotNull              ] public string    DeviceProduceLogId { get; set; } // varchar(100)
		/// <summary>
		/// 日志时间
		/// </summary>
		[Column(),        Nullable          ] public DateTime? Created            { get; set; } // datetime(6)
		/// <summary>
		/// 采集来源传感器编号
		/// </summary>
		[Column(),        Nullable          ] public string    SensorId           { get; set; } // varchar(100)
		/// <summary>
		/// 传感器当前状态
		/// </summary>
		[Column(),        Nullable          ] public string    SensorStatus       { get; set; } // varchar(5)
		/// <summary>
		/// 参数类型
		/// </summary>
		[Column(),        Nullable          ] public string    ParType            { get; set; } // varchar(5)
		/// <summary>
		/// 参数单位
		/// </summary>
		[Column(),        Nullable          ] public string    ParUnit            { get; set; } // varchar(5)
		[Column(),        Nullable          ] public decimal?  ParValue           { get; set; } // decimal(10,0)
	}

	/// <summary>
	/// 工艺子流程执行日志表
	/// </summary>
	[Table("l_exprocesslog")]
	public partial class LExprocesslog
	{
		[Column("id"), PrimaryKey,  Identity] public int       Id                  { get; set; } // int(11)
		[Column(),     NotNull              ] public string    ExProcessLogId      { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string    ProcessLogId        { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string    ExProcessId         { get; set; } // varchar(100)
		[Column(),        Nullable          ] public DateTime? Created             { get; set; } // datetime(6)
		[Column(),        Nullable          ] public string    CreateUser          { get; set; } // varchar(100)
		[Column(),        Nullable          ] public DateTime? FinishTime          { get; set; } // datetime(6)
		[Column(),        Nullable          ] public string    ProcessStatus       { get; set; } // varchar(10)
		[Column(),        Nullable          ] public string    ProduceMaterialType { get; set; } // varchar(10)
		[Column(),        Nullable          ] public decimal?  Production          { get; set; } // decimal(10,0)
		[Column(),        Nullable          ] public int?      TakeTime            { get; set; } // int(11)
	}

	/// <summary>
	/// 工艺子流程步骤日志表
	/// </summary>
	[Table("l_exprocesssteplog")]
	public partial class LExprocesssteplog
	{
		[Column("id"), PrimaryKey,  Identity] public int       Id                 { get; set; } // int(11)
		[Column(),     NotNull              ] public string    ExProcessStepLogId { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string    ExProcessLogId     { get; set; } // varchar(100)
		[Column(),        Nullable          ] public DateTime? Created            { get; set; } // datetime(6)
		[Column(),        Nullable          ] public string    CreateUser         { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string    ProcessDeviceId    { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string    DeviceId           { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string    StepId             { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string    ActionType         { get; set; } // varchar(10)
	}

	/// <summary>
	/// 工艺子流程步骤参数日志表
	/// </summary>
	[Table("l_exprocessstepparslog")]
	public partial class LExprocessstepparslog
	{
		[Column("id"), PrimaryKey,  Identity] public int       Id                 { get; set; } // int(11)
		[Column(),     NotNull              ] public string    ParId              { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string    ExProcessStepLogId { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string    SensorId           { get; set; } // varchar(100)
		[Column(),        Nullable          ] public DateTime? Created            { get; set; } // datetime(6)
		[Column(),        Nullable          ] public string    ParType            { get; set; } // varchar(10)
		[Column(),        Nullable          ] public string    ParUnit            { get; set; } // varchar(5)
		/// <summary>
		/// 浮点数值类参数值(重量，温度，搅拌速度,PH值等)
		/// </summary>
		[Column(),        Nullable          ] public decimal?  ParValue           { get; set; } // decimal(10,0)
	}

	/// <summary>
	/// 工艺流程执行日志主表
	/// </summary>
	[Table("l_processlog")]
	public partial class LProcesslog
	{
		[Column("id"), PrimaryKey,  Identity] public int       Id                  { get; set; } // int(11)
		/// <summary>
		/// 工业流程生产日志编号
		/// </summary>
		[Column(),     NotNull              ] public string    ProcessLogId        { get; set; } // varchar(100)
		/// <summary>
		/// 生产线编号
		/// </summary>
		[Column(),     NotNull              ] public string    AssemblyLineId      { get; set; } // varchar(100)
		/// <summary>
		/// 主工艺流程编号
		/// </summary>
		[Column(),     NotNull              ] public string    ProcessId           { get; set; } // varchar(100)
		/// <summary>
		/// 流程启动时间
		/// </summary>
		[Column(),     NotNull              ] public DateTime  Created             { get; set; } // datetime(6)
		[Column(),        Nullable          ] public string    CreateUser          { get; set; } // varchar(100)
		/// <summary>
		/// 流程完成时间
		/// </summary>
		[Column(),        Nullable          ] public DateTime? FinishTime          { get; set; } // datetime(6)
		/// <summary>
		/// 流程运行状态
		/// </summary>
		[Column(),        Nullable          ] public string    ProcessStatus       { get; set; } // varchar(10)
		/// <summary>
		/// 生产出物料类型
		/// </summary>
		[Column(),        Nullable          ] public string    ProduceMaterialType { get; set; } // varchar(10)
		/// <summary>
		/// 生产的物料产量（KG）
		/// </summary>
		[Column(),        Nullable          ] public decimal?  Production          { get; set; } // decimal(10,0)
		/// <summary>
		/// 总共花费的时间（分钟单位）
		/// </summary>
		[Column(),        Nullable          ] public int?      TakeTime            { get; set; } // int(11)
	}

	[Table("p_exprocess")]
	public partial class PExprocess
	{
		[Column("id"), PrimaryKey,  Identity] public int     Id             { get; set; } // int(11)
		[Column(),     NotNull              ] public string  ExProcessId    { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string  ExProcessTitle { get; set; } // varchar(200)
		[Column(),     NotNull              ] public string  SortCode       { get; set; } // varchar(5)
		[Column(),     NotNull              ] public string  ProcessId      { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string  StartDeviceId  { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string  ParType        { get; set; } // varchar(10)
		[Column(),     NotNull              ] public decimal ParValue       { get; set; } // decimal(10,0)
		[Column(),     NotNull              ] public int     ProcessOrderId { get; set; } // int(11)
	}

	[Table("p_exprocessstep")]
	public partial class PExprocessstep
	{
		[Column("id"), PrimaryKey,  Identity] public int      Id               { get; set; } // int(11)
		[Column(),     NotNull              ] public string   StepId           { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string   StepTitle        { get; set; } // varchar(200)
		[Column(),     NotNull              ] public string   ExProcessId      { get; set; } // varchar(100)
		[Column(),     NotNull              ] public int      OrderIndex       { get; set; } // int(11)
		[Column(),     NotNull              ] public string   ProcessDeviceId  { get; set; } // varchar(100)
		/// <summary>
		/// 操作类型（排出/搅拌等）
		/// </summary>
		[Column(),     NotNull              ] public string   ActionType       { get; set; } // varchar(10)
		[Column(),     NotNull              ] public sbyte    IsSync           { get; set; } // tinyint(4)
		/// <summary>
		/// 同步的步骤的间隔启动时间（分钟）
		/// </summary>
		[Column(),     NotNull              ] public int      SyncStepInterval { get; set; } // int(11)
		[Column(),        Nullable          ] public string   FinishParType    { get; set; } // varchar(10)
		[Column(),        Nullable          ] public string   FinishParUnit    { get; set; } // varchar(10)
		[Column(),        Nullable          ] public decimal? FinishParValue   { get; set; } // decimal(10,0)
		[Column(),     NotNull              ] public string   BeforeStepId     { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string   ResultsId        { get; set; } // varchar(100)
	}

	[Table("p_exprocesssteppars")]
	public partial class PExprocesssteppar
	{
		[Column("id"), PrimaryKey,  Identity] public int      Id         { get; set; } // int(11)
		[Column(),     NotNull              ] public string   ParId      { get; set; } // varchar(100)
		[Column(),     NotNull              ] public string   StepId     { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string   SensorId   { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string   ActionType { get; set; } // varchar(5)
		[Column(),     NotNull              ] public string   ParType    { get; set; } // varchar(10)
		[Column(),     NotNull              ] public string   ParUnit    { get; set; } // varchar(30)
		/// <summary>
		/// 浮点数值类参数值(重量，温度，搅拌速度,PH值)
		/// </summary>
		[Column(),        Nullable          ] public decimal? ParValue   { get; set; } // decimal(10,0)
		/// <summary>
		/// 需要的时间数（记录分钟计算）
		/// </summary>
		[Column(),     NotNull              ] public int      ParTime    { get; set; } // int(11)
		/// <summary>
		/// 参数是否是步骤完成的条件
		/// </summary>
		[Column(),     NotNull              ] public sbyte    IsFinish   { get; set; } // tinyint(4)
	}

	[Table("p_mainprocess")]
	public partial class PMainprocess
	{
		[Column("id"), PrimaryKey,  Identity] public int     Id           { get; set; } // int(11)
		[Column(),     NotNull              ] public string  ProcessId    { get; set; } // varchar(100)
		[Column(),        Nullable          ] public string  ProcessTitle { get; set; } // varchar(200)
		[Column(),     NotNull              ] public string  ProductsType { get; set; } // varchar(30)
		[Column(),     NotNull              ] public decimal PurityMin    { get; set; } // decimal(10,0)
		[Column(),     NotNull              ] public decimal PuritvMax    { get; set; } // decimal(10,0)
		[Column(),     NotNull              ] public int     ProcessYear  { get; set; } // int(11)
		[Column(),     NotNull              ] public int     Fineness     { get; set; } // int(11)
	}

	/// <summary>
	/// VIEW
	/// </summary>
	[Table("v_assemblylinedevices", IsView=true)]
	public partial class VAssemblylinedevice
	{
		[Column("id"), NotNull    ] public int    Id              { get; set; } // int(11)
		[Column(),     NotNull    ] public string DeviceId        { get; set; } // varchar(100)
		[Column(),        Nullable] public string DeviceName      { get; set; } // varchar(200)
		[Column(),        Nullable] public string DeviceType      { get; set; } // varchar(5)
		[Column(),        Nullable] public string ProcessDeviceId { get; set; } // varchar(100)
		[Column(),        Nullable] public string DeviceStatus    { get; set; } // varchar(5)
		[Column(),        Nullable] public string Parameter1      { get; set; } // varchar(100)
		[Column(),        Nullable] public string Parameter2      { get; set; } // varchar(100)
		[Column(),     NotNull    ] public string AssemblyLineId  { get; set; } // varchar(100)
	}

	public static partial class TableExtensions
	{
		public static BDeviceset Find(this ITable<BDeviceset> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static FAssemblylinedevice Find(this ITable<FAssemblylinedevice> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static FAssemblylineinfo Find(this ITable<FAssemblylineinfo> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static FDeviceinfo Find(this ITable<FDeviceinfo> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static FFactoryinfo Find(this ITable<FFactoryinfo> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static FSensorinfo Find(this ITable<FSensorinfo> table, int Sid)
		{
			return table.FirstOrDefault(t =>
				t.Sid == Sid);
		}

		public static LDeviceactionlog Find(this ITable<LDeviceactionlog> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LDeviceproducelog Find(this ITable<LDeviceproducelog> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LDeviceproducelogpar Find(this ITable<LDeviceproducelogpar> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LExprocesslog Find(this ITable<LExprocesslog> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LExprocesssteplog Find(this ITable<LExprocesssteplog> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LExprocessstepparslog Find(this ITable<LExprocessstepparslog> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LProcesslog Find(this ITable<LProcesslog> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PExprocess Find(this ITable<PExprocess> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PExprocessstep Find(this ITable<PExprocessstep> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PExprocesssteppar Find(this ITable<PExprocesssteppar> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PMainprocess Find(this ITable<PMainprocess> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}

#pragma warning restore 1591
