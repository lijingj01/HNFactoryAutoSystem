<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FactoryDeviceManage.aspx.cs" Inherits="HNFactoryAutoSystem.Web.FactoryDeviceManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工厂设备管理</title>
    <link href="Scripts/ace_admin_cn/assets/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="Scripts/bootstrap-4.4.1/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="Scripts/ace_admin_cn/assets/css/font-awesome.min.css" rel="stylesheet" />
    <script src="Scripts/ace_admin_cn/assets/js/jquery.min.js"></script>

    <script src="Scripts/bootstrap-table/bootstrap-table.js"></script>
    <link href="Scripts/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-table/locale/bootstrap-table-zh-CN.js"></script>

    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/jquery-ui-1.10.3.custom.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/chosen.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/jquery.gritter.css" />
    <style>
        .changeColor {
            background-color: #31b0d5 !important;
            color: white;
        }

        #tb_exprocesslist tr:nth-child(even) {
            background: #fafafa;
        }

        #tb_exprocesslist th {
            background: #efefef;
        }

        #tb_exprocesssteplist tr:nth-child(even) {
            background: #fafafa;
        }

        #tb_exprocesssteplist th {
            background: #efefef;
        }
    </style>

    <script src="Scripts/ace_admin_cn/assets/js/ace-extra.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/bootstrap.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/typeahead-bs2.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.slimscroll.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.easy-pie-chart.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.sparkline.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.pie.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.resize.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/bootbox.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/chosen.jquery.min.js"></script>

    <script src="Scripts/JsHelper.js"></script>
    <script src="Scripts/FDMHelper.js"></script>

    <script>
        var $path_assets = "Scripts/ace_admin_cn/assets";//this will be used in gritter alerts containing images

        $(document).ready(function () {
            $().ready(function () {

                //1.初始化Table
                var oTable = new LineTableInit("");
                oTable.Init();

                //2.初始化Button的点击事件
                //var oButtonInit = new ButtonInit();
                //oButtonInit.Init();

                $(".chosen-select").chosen();
                //设置弹出框里面下拉框控件宽度，样式有冲突
                $(".chosen-container").css("width", "400px");

                $('.widget-box a').click(function () {
                    var code = $(this).data('code');

                    //更新按键样式
                    $('.widget-box a').removeClass('btn-danger');
                    $('.widget-box a').addClass('btn-primary');

                    $(this).removeClass('btn-primary');
                    $(this).addClass('btn-danger');
                    //加载生产线
                    //alert(code);
                    LoadLineList(code);

                    LoadFactoryDevices(code);
                });

                $("#btn_editStep").click(function () {
                    //判断是否选择了传感器
                    var sensor = $("#tb_sensorlist").bootstrapTable('getSelections');//获取选中行的数据
                    if (sensor.length > 0) {
                        $("#txt_sensorid").val(sensor[0].SensorId);

                        //所属设备选中
                        $('#sel-Device').val(sensor[0].DeviceId);//赋值
                        $('#sel-Device').trigger("chosen:updated");//设置选中

                        //连接设备选中
                        if (sensor[0].ToDeviceId != null) {
                            $('#sel-ToDevice').val(sensor[0].ToDeviceId);//赋值
                            $('#sel-ToDevice').trigger("chosen:updated");//设置选中
                        }

                        $('#myModal').modal();
                    } else {

                        showMsg("请先选择传感器!", function () { });
                    }

                });

                $("#btn_editdevice").click(function () {
                    //判断是否选择了设备
                    var device = $("#tb_devicelist").bootstrapTable('getSelections');//获取选中行的数据
                    if (device.length > 0) {
                        $("#txt_deviceid").val(device[0].DeviceId);
                        $("#txt_devicename").val(device[0].DeviceName);

                        $('#deviceModal').modal();
                    } else {

                        showMsg("请先选择设备!", function () { });
                    }

                });

                //提交传感器关联参数
                $("#btn_submit").click(function () {
                    var sensorid = $("#txt_sensorid").val();
                    var sDeviceId = chose_get_value("#sel-Device");
                    var sToDeviceId = chose_get_value("#sel-ToDevice");

                    ChangeSensor(sensorid, sDeviceId, sToDeviceId);

                });
                //更新设备信息
                $("#btn_devicesubmit").click(function () {
                    var deviceId = $("#txt_deviceid").val();
                    var deviceName = $("#txt_devicename").val();

                    ChangeDevice(deviceId, deviceName);

                });

                ModelSetCenter("myModal");
                ModelSetCenter("deviceModal");
                ModelSetCenter("com-alert");

            });
        });

        function ChangeDevice(deviceId, deviceName) {
            $.ajax({
                url: '<%= ResolveUrl("~/Ashx/FactoryHandler.ashx") %>',
                dataType: "json",
                type: "POST",
                data: {
                    Action: "ChangeDeviceInfo",
                    DeviceId: deviceId,
                    DeviceName: deviceName
                },
                success: function (result) {
                    if (result.code == 0) {
                        showMsg("设备信息更新成功!", function () { RefreshDeviceList(); });
                    } else {
                        showMsg(result.message, function () { });
                    }
                }
            });
        }

        function ChangeSensor(sensorid, deviceid, todeviceid) {


            $.ajax({
                url: '<%= ResolveUrl("~/Ashx/FactoryHandler.ashx") %>',
                dataType: "json",
                type: "POST",
                data: {
                    Action: "ChangeSensorJoin",
                    SensorId: sensorid,
                    DeviceId: deviceid,
                    ToDeviceId: todeviceid
                },
                success: function (result) {
                    if (result.code == 0) {
                        //$("#messageInfo").text("传感器信息更新成功！");


                        showMsg("传感器信息更新成功!", function () { RefreshSensorList(); });
                    } else {
                        //$("#messageInfo").text(result.message);

                        showMsg(result.message, function () { });
                    }
                    //$("#messageModal").modal();
                }
            });
        }


  </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" id="navbar">
            <script type="text/javascript">
                try { ace.settings.check('navbar', 'fixed') } catch (e) { }
            </script>

            <div class="navbar-container" id="navbar-container">
                <div class="navbar-header pull-left">
                    <a href="#" class="navbar-brand">
                        <small>
                            <i class="icon-leaf"></i>
                            智慧制造工艺后台管理系统
                        </small>
                    </a>
                    <!-- /.brand -->
                </div>
                <!-- /.navbar-header -->

                <div class="navbar-header pull-right" role="navigation">
                    <ul class="nav ace-nav">
                        <li class="grey">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="icon-tasks"></i>
                                <span class="badge badge-grey">4</span>
                            </a>

                            <ul class="pull-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                                <li class="dropdown-header">
                                    <i class="icon-ok"></i>
                                    还有4个任务完成
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">软件更新</span>
                                            <span class="pull-right">65%</span>
                                        </div>

                                        <div class="progress progress-mini ">
                                            <div style="width: 65%" class="progress-bar "></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">硬件更新</span>
                                            <span class="pull-right">35%</span>
                                        </div>

                                        <div class="progress progress-mini ">
                                            <div style="width: 35%" class="progress-bar progress-bar-danger"></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">单元测试</span>
                                            <span class="pull-right">15%</span>
                                        </div>

                                        <div class="progress progress-mini ">
                                            <div style="width: 15%" class="progress-bar progress-bar-warning"></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">错误修复</span>
                                            <span class="pull-right">90%</span>
                                        </div>

                                        <div class="progress progress-mini progress-striped active">
                                            <div style="width: 90%" class="progress-bar progress-bar-success"></div>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">查看任务详情
										<i class="icon-arrow-right"></i>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <li class="purple">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="icon-bell-alt icon-animated-bell"></i>
                                <span class="badge badge-important">8</span>
                            </a>

                            <ul class="pull-right dropdown-navbar navbar-pink dropdown-menu dropdown-caret dropdown-close">
                                <li class="dropdown-header">
                                    <i class="icon-warning-sign"></i>
                                    8条通知
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-pink icon-comment"></i>
                                                新闻评论
                                            </span>
                                            <span class="pull-right badge badge-info">+12</span>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <i class="btn btn-xs btn-primary icon-user"></i>
                                        切换为编辑登录..
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-success icon-shopping-cart"></i>
                                                新订单
                                            </span>
                                            <span class="pull-right badge badge-success">+8</span>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-info icon-twitter"></i>
                                                粉丝
                                            </span>
                                            <span class="pull-right badge badge-info">+11</span>
                                        </div>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">查看所有通知
										<i class="icon-arrow-right"></i>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <li class="green">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="icon-envelope icon-animated-vertical"></i>
                                <span class="badge badge-success">5</span>
                            </a>

                            <ul class="pull-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                                <li class="dropdown-header">
                                    <i class="icon-envelope-alt"></i>
                                    5条消息
                                </li>

                                <li>
                                    <a href="#">
                                        <img src="Scripts/ace_admin_cn/assets/avatars/avatar.png" class="msg-photo" alt="Alex's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Alex:</span>
                                                不知道写啥 ...
                                            </span>

                                            <span class="msg-time">
                                                <i class="icon-time"></i>
                                                <span>1分钟以前</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <img src="Scripts/ace_admin_cn/assets/avatars/avatar3.png" class="msg-photo" alt="Susan's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Susan:</span>
                                                不知道翻译...
                                            </span>

                                            <span class="msg-time">
                                                <i class="icon-time"></i>
                                                <span>20分钟以前</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <img src="Scripts/ace_admin_cn/assets/avatars/avatar4.png" class="msg-photo" alt="Bob's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Bob:</span>
                                                到底是不是英文 ...
                                            </span>

                                            <span class="msg-time">
                                                <i class="icon-time"></i>
                                                <span>下午3:15</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a href="inbox.html">查看所有消息
										<i class="icon-arrow-right"></i>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <li class="light-blue">
                            <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                                <img class="nav-user-photo" src="Scripts/ace_admin_cn/assets/avatars/user.jpg" alt="Jason's Photo" />
                                <span class="user-info">
                                    <small>欢迎光临,</small>
                                    Jason
                                </span>

                                <i class="icon-caret-down"></i>
                            </a>

                            <ul class="user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
                                <li>
                                    <a href="#">
                                        <i class="icon-cog"></i>
                                        设置
                                    </a>
                                </li>

                                <li>
                                    <a href="#">
                                        <i class="icon-user"></i>
                                        个人资料
                                    </a>
                                </li>

                                <li class="divider"></li>

                                <li>
                                    <a href="#">
                                        <i class="icon-off"></i>
                                        退出
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                    <!-- /.ace-nav -->
                </div>
                <!-- /.navbar-header -->
            </div>
            <!-- /.container -->
        </div>

        <div class="main-container" id="main-container">
            <script type="text/javascript">
                try { ace.settings.check('main-container', 'fixed') } catch (e) { }
            </script>

            <div class="main-container-inner">
                <a class="menu-toggler" id="menu-toggler" href="#">
                    <span class="menu-text"></span>
                </a>

                <div class="sidebar" id="sidebar">
                    <script type="text/javascript">
                        try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
                    </script>

                    <!-- #sidebar-shortcuts -->

                    <ul class="nav nav-list">
                        <li>
                            <a href="ProcessManage.aspx">
                                <i class="icon-dashboard"></i>
                                <span class="menu-text">工艺设置 </span>
                            </a>
                        </li>
                        <li class="active">
                            <a href="FactoryDeviceManage.aspx">
                                <i class="icon-text-width"></i>
                                <span class="menu-text"><%=PageTitle %></span>
                            </a>
                        </li>
                    </ul>
                    <!-- /.nav-list -->

                    <div class="sidebar-collapse" id="sidebar-collapse">
                        <i class="icon-double-angle-left" data-icon1="icon-double-angle-left" data-icon2="icon-double-angle-right"></i>
                    </div>

                    <script type="text/javascript">
                        try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
                    </script>
                </div>

                <div class="main-content">
                    <div class="breadcrumbs" id="breadcrumbs">
                        <script type="text/javascript">
                            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                        </script>

                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home home-icon"></i>
                                <a href="#">首页</a>
                            </li>
                            <li class="active"><%=PageTitle %></li>
                        </ul>
                        <!-- .breadcrumb -->
                    </div>

                    <div class="page-content">
                        <div class="page-header">
                            <h1>控制台
								<small>
                                    <i class="icon-double-angle-right"></i>
                                    查看
                                </small>
                            </h1>
                        </div>
                        <!-- /.page-header -->
                        <div class="row">
                            <div class="card-deck mb-3 text-center">
                                <%=CardList %>
                            </div>
                        </div>


                        <div class="row">

                            <div class="col-sm-12 pricing-box">
                                <div class="widget-box">
                                    <div class="widget-header header-color-green">
                                        <h5 class="bigger lighter">生产线列表</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="linetoolbar" class="btn-group">
                                                <button id="btn_addPar" type="button" class="btn btn-app btn-danger btn-xs" title="新增">
                                                    <span class="icon-plus" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_editPar" type="button" class="btn btn-app btn-primary btn-xs" title="修改">
                                                    <span class="icon-edit" aria-hidden="true"></span>
                                                </button>
                                                <button id="btn_deletePar" type="button" class="btn btn-app btn-grey btn-xs" title="删除">
                                                    <span class="icon-trash" aria-hidden="true"></span>
                                                </button>
                                            </div>
                                            <table id="tb_linelist"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>


                        <div class="row">
                            <div class="col-sm-12 pricing-box">
                                <div class="widget-box">
                                    <div class="widget-header header-color-orange">
                                        <h5 class="bigger lighter">设备列表</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="devicetoolbar" class="btn-group">
                                                <%--<button id="btn_add" type="button" class="btn btn-app btn-danger btn-xs" title="新增">
                                                    <span class="icon-plus" aria-hidden="true"></span>新增
                                                </button>
                                                <button id="btn_edit" type="button" class="btn btn-app btn-primary btn-xs" title="修改">
                                                    <span class="icon-edit" aria-hidden="true"></span>修改
                                                </button>--%>
                                                <button id="btn_join" type="button" class="btn btn-app btn-success btn-xs" title="关联设备到生产线">
                                                    <span class="icon-check" aria-hidden="true"></span>关联
                                                </button>
                                                <button id="btn_editdevice" type="button" class="btn btn-app btn-primary btn-xs" data-toggle="modal" data-target="#deviceModal" title="编辑设备信息">
                                                    <span class="icon-edit" aria-hidden="true"></span>编辑
                                                </button>
                                                <button id="btn_delete" type="button" class="btn btn-app btn-grey btn-xs" title="移除生产线设备">
                                                    <span class="icon-check-empty" aria-hidden="true"></span>移除
                                                </button>
                                            </div>
                                            <table id="tb_devicelist"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="row">

                            <div class="col-sm-12 pricing-box">
                                <div class="widget-box">
                                    <div class="widget-header header-color-green">
                                        <h5 class="bigger lighter">传感器列表</h5>
                                    </div>
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="sonsertoolbar" class="btn-group">
                                                <button id="btn_addStep" type="button" class="btn btn-app btn-danger btn-xs" title="关联传感器到设备">
                                                    <span class="icon-check" aria-hidden="true"></span>关联
                                                </button>
                                                <button id="btn_editStep" type="button" class="btn btn-app btn-primary btn-xs" data-toggle="modal" data-target="#mySensorModal" title="编辑传感器信息">
                                                    <span class="icon-edit" aria-hidden="true"></span>编辑
                                                </button>
                                                <%--                                                <button id="btn_deleteStep" type="button" class="btn btn-app btn-grey btn-xs" title="删除">
                                                    <span class="icon-trash" aria-hidden="true"></span>
                                                </button>--%>
                                            </div>
                                            <table id="tb_sensorlist"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </div>



                    </div>
                    <!-- /.page-content -->
                </div>
                <!-- /.main-content -->

                <div class="ace-settings-container" id="ace-settings-container">
                    <div class="btn btn-app btn-xs btn-warning ace-settings-btn" id="ace-settings-btn">
                        <i class="icon-cog bigger-150"></i>
                    </div>

                    <div class="ace-settings-box" id="ace-settings-box">
                        <div>
                            <div class="pull-left">
                                <select id="skin-colorpicker" class="hide">
                                    <option data-skin="default" value="#438EB9">#438EB9</option>
                                    <option data-skin="skin-1" value="#222A2D">#222A2D</option>
                                    <option data-skin="skin-2" value="#C6487E">#C6487E</option>
                                    <option data-skin="skin-3" value="#D0D0D0">#D0D0D0</option>
                                </select>
                            </div>
                            <span>&nbsp; 选择皮肤</span>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-navbar" />
                            <label class="lbl" for="ace-settings-navbar">固定导航条</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-sidebar" />
                            <label class="lbl" for="ace-settings-sidebar">固定滑动条</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-breadcrumbs" />
                            <label class="lbl" for="ace-settings-breadcrumbs">固定面包屑</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-rtl" />
                            <label class="lbl" for="ace-settings-rtl">切换到左边</label>
                        </div>

                        <div>
                            <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-add-container" />
                            <label class="lbl" for="ace-settings-add-container">
                                切换窄屏
								<b></b>
                            </label>
                        </div>
                    </div>
                </div>
                <!-- /#ace-settings-container -->
            </div>
            <!-- /.main-container-inner -->

            <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
                <i class="icon-double-angle-up icon-only bigger-110"></i>
            </a>
        </div>

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">传感器关联操作</h4>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label for="txt_sensorid">传感器编号</label>
                            <input type="text" name="txt_sensorid" class="form-control" id="txt_sensorid" readonly="readonly" placeholder="传感器编号" />
                        </div>
                        <div class="form-group">
                            <div>
                                <label for="sel-Device">关联设备</label>
                                <br />
                                <select class="width-80 chosen-select" id="sel-Device" data-placeholder="Choose a Country...">
                                    <option value="">&nbsp;</option>
                                    <option value="AL">Alabama</option>
                                    <option value="AK">Alaska</option>
                                    <option value="AZ">Arizona</option>
                                    <option value="AR">Arkansas</option>
                                    <option value="CA">California</option>
                                    <option value="CO">Colorado</option>
                                    <option value="CT">Connecticut</option>
                                    <option value="DE">Delaware</option>
                                    <option value="FL">Florida</option>
                                    <option value="GA">Georgia</option>
                                    <option value="HI">Hawaii</option>
                                    <option value="ID">Idaho</option>
                                    <option value="IL">Illinois</option>
                                    <option value="IN">Indiana</option>
                                    <option value="IA">Iowa</option>
                                    <option value="KS">Kansas</option>
                                    <option value="KY">Kentucky</option>
                                    <option value="LA">Louisiana</option>
                                    <option value="ME">Maine</option>
                                    <option value="MD">Maryland</option>
                                    <option value="MA">Massachusetts</option>
                                    <option value="MI">Michigan</option>
                                    <option value="MN">Minnesota</option>
                                    <option value="MS">Mississippi</option>
                                    <option value="MO">Missouri</option>
                                    <option value="MT">Montana</option>
                                    <option value="NE">Nebraska</option>
                                    <option value="NV">Nevada</option>
                                    <option value="NH">New Hampshire</option>
                                    <option value="NJ">New Jersey</option>
                                    <option value="NM">New Mexico</option>
                                    <option value="NY">New York</option>
                                    <option value="NC">North Carolina</option>
                                    <option value="ND">North Dakota</option>
                                    <option value="OH">Ohio</option>
                                    <option value="OK">Oklahoma</option>
                                    <option value="OR">Oregon</option>
                                    <option value="PA">Pennsylvania</option>
                                    <option value="RI">Rhode Island</option>
                                    <option value="SC">South Carolina</option>
                                    <option value="SD">South Dakota</option>
                                    <option value="TN">Tennessee</option>
                                    <option value="TX">Texas</option>
                                    <option value="UT">Utah</option>
                                    <option value="VT">Vermont</option>
                                    <option value="VA">Virginia</option>
                                    <option value="WA">Washington</option>
                                    <option value="WV">West Virginia</option>
                                    <option value="WI">Wisconsin</option>
                                    <option value="WY">Wyoming</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="sel-ToDevice">连接设备</label>
                            <br />
                            <select class="width-80 chosen-select" id="sel-ToDevice" data-placeholder="Choose a Country...">
                                <option value="">&nbsp;</option>
                                <option value="AL">Alabama</option>
                                <option value="AK">Alaska</option>
                                <option value="AZ">Arizona</option>
                                <option value="AR">Arkansas</option>
                                <option value="CA">California</option>
                                <option value="CO">Colorado</option>
                                <option value="CT">Connecticut</option>
                                <option value="DE">Delaware</option>
                                <option value="FL">Florida</option>
                                <option value="GA">Georgia</option>
                                <option value="HI">Hawaii</option>
                                <option value="ID">Idaho</option>
                                <option value="IL">Illinois</option>
                                <option value="IN">Indiana</option>
                                <option value="IA">Iowa</option>
                                <option value="KS">Kansas</option>
                                <option value="KY">Kentucky</option>
                                <option value="LA">Louisiana</option>
                                <option value="ME">Maine</option>
                                <option value="MD">Maryland</option>
                                <option value="MA">Massachusetts</option>
                                <option value="MI">Michigan</option>
                                <option value="MN">Minnesota</option>
                                <option value="MS">Mississippi</option>
                                <option value="MO">Missouri</option>
                                <option value="MT">Montana</option>
                                <option value="NE">Nebraska</option>
                                <option value="NV">Nevada</option>
                                <option value="NH">New Hampshire</option>
                                <option value="NJ">New Jersey</option>
                                <option value="NM">New Mexico</option>
                                <option value="NY">New York</option>
                                <option value="NC">North Carolina</option>
                                <option value="ND">North Dakota</option>
                                <option value="OH">Ohio</option>
                                <option value="OK">Oklahoma</option>
                                <option value="OR">Oregon</option>
                                <option value="PA">Pennsylvania</option>
                                <option value="RI">Rhode Island</option>
                                <option value="SC">South Carolina</option>
                                <option value="SD">South Dakota</option>
                                <option value="TN">Tennessee</option>
                                <option value="TX">Texas</option>
                                <option value="UT">Utah</option>
                                <option value="VT">Vermont</option>
                                <option value="VA">Virginia</option>
                                <option value="WA">Washington</option>
                                <option value="WV">West Virginia</option>
                                <option value="WI">Wisconsin</option>
                                <option value="WY">Wyoming</option>
                            </select>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal"><span class="icon-remove" aria-hidden="true"></span>关闭</button>
                        <button type="button" id="btn_submit" class="btn btn-primary" data-dismiss="modal"><span class="icon-save" aria-hidden="true"></span>保存</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deviceModal" tabindex="-1" role="dialog" aria-labelledby="deviceModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="deviceModalLabel">设备基础信息编辑</h4>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label for="txt_deviceid">设备编号</label>
                            <input type="text" name="txt_deviceid" class="form-control" id="txt_deviceid" readonly="readonly" placeholder="设备编号" />
                        </div>
                        <div class="form-group">
                            <label for="txt_devicename">设备名称</label>
                            <input type="text" name="txt_devicename" class="form-control" id="txt_devicename" placeholder="设备名称" />
                        </div>


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal"><span class="icon-remove" aria-hidden="true"></span>关闭</button>
                        <button type="button" id="btn_devicesubmit" class="btn btn-primary" data-dismiss="modal"><span class="icon-save" aria-hidden="true"></span>保存</button>
                    </div>
                </div>
            </div>
        </div>

        <!--模态框组件-->
        <%--        <div class="modal fade" id="messageModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3>提示信息</h3>
                    </div>
                    <div class="modal-body">
                        <h4 class="modal-title" style="color: red" id="messageInfo">错误信息</h4>

                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-info" data-dismiss="modal">关闭</button>
                    </div>
                </div>
            </div>
        </div>--%>

        <div id="com-alert" class="modal" style="z-index: 9999; display: none;">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                        <h5 class="modal-title"><i class="fa fa-exclamation-circle"></i>[Title]</h5>
                    </div>
                    <div class="modal-body small">
                        <p>[Message]</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary ok" data-dismiss="modal">[BtnOk]</button>
                        <button type="button" class="btn btn-default cancel" data-dismiss="modal">[BtnCancel]</button>
                    </div>
                </div>
            </div>
        </div>


    </form>
</body>
</html>
