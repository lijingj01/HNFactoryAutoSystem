<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="HNFactoryAutoSystem.Web.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <title>工艺流程库</title>
    <link href="Scripts/bootstrap-4.4.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap-4.4.1/js/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap-table/bootstrap-table.js"></script>
    <link href="Scripts/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-table/locale/bootstrap-table-zh-CN.js"></script>
    <script>
        $(document).ready(function () {
            $().ready(function () {
                $sidebar = $('.sidebar');
                $navbar = $('.navbar');
                $main_panel = $('.main-panel');

                $full_page = $('.full-page');

                $sidebar_responsive = $('body > .navbar-collapse');
                sidebar_mini_active = true;
                white_color = false;

                window_width = $(window).width();

                fixed_plugin_open = $('.sidebar .sidebar-wrapper .nav li.active a p').html();

                var new_color = "blue";
                SetColor(new_color);

                //1.初始化Table
                var oTable = new ExTableInit("");
                oTable.Init();

                //2.初始化Button的点击事件
                var oButtonInit = new ButtonInit();
                oButtonInit.Init();

                $('.card-body button').click(function () {
                    var code = $(this).data('code');

                    //更新按键样式
                    $(this).removeClass('btn-outline-primary');
                    $(this).addClass('btn-outline-dark');
                    //加载工艺列表
                    //alert(code);
                    LoadProcessList(code);
                });
            });
        });

        function SetColor(new_color) {
            $sidebar = $('.sidebar');
            $navbar = $('.navbar');
            $main_panel = $('.main-panel');

            $full_page = $('.full-page');

            $sidebar_responsive = $('body > .navbar-collapse');

            if ($sidebar.length != 0) {
                $sidebar.attr('data', new_color);
            }

            if ($main_panel.length != 0) {
                $main_panel.attr('data', new_color);
            }

            if ($full_page.length != 0) {
                $full_page.attr('filter-color', new_color);
            }

            if ($sidebar_responsive.length != 0) {
                $sidebar_responsive.attr('data', new_color);
            }
        }
        function LoadProcessList(pcode) {
            $.ajax({
                type: "post",
                url: "/Ashx/ProcessHandler.ashx",
                data: {
                    ProcessId: pcode,
                    Action: "GetExprocessList"
                },
                dataType: "json",
                success: function (json) {
                    $("#tb_exprocesslist").bootstrapTable('load', json);
                }
            });

        }
        function LoadExProcessSteps(pcode) {
            $.ajax({
                type: "post",
                url: "/Ashx/ProcessHandler.ashx",
                data: {
                    ExProcessId: pcode,
                    Action: "GetExProcessSteps"
                },
                dataType: "json",
                success: function (json) {
                    $("#tb_exprocesssteplist").bootstrapTable('load', json);
                }
            });
        }
  </script>
    <script>

        var ExTableInit = function (pcode) {
            var oTableInit = new Object();

            var oStepTable = new setpTableInit("");
            oStepTable.Init();

            //初始化Table
            oTableInit.Init = function () {
                $('#tb_exprocesslist').bootstrapTable({
                    url: '/Ashx/ProcessHandler.ashx',         //请求后台的URL（*）
                    method: 'get',                      //请求方式（*）
                    toolbar: '#toolbar',                //工具按钮用哪个容器
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: true,                   //是否显示分页（*）
                    sortable: false,                     //是否启用排序
                    sortOrder: "asc",                   //排序方式
                    queryParams: oTableInit.queryParams,//传递参数（*）
                    sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
                    pageNumber: 1,                       //初始化加载第一页，默认第一页
                    pageSize: 10,                       //每页的记录行数（*）
                    pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                    search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
                    strictSearch: true,
                    showColumns: false,                  //是否显示所有的列(列选择器)
                    showRefresh: false,                  //是否显示刷新按钮
                    minimumCountColumns: 2,             //最少允许的列数
                    clickToSelect: true,                //是否启用点击选中行
                    height: 350,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                    uniqueId: "ExProcessId",                     //每一行的唯一标识，一般为主键列
                    showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
                    cardView: false,                    //是否显示详细视图
                    detailView: false,                   //是否显示父子表
                    singleSelect: true,                    //是否单选
                    columns: [{
                        field: 'ExProcessId',
                        title: '子流程编码'
                    }, {
                        field: 'ExProcessTitle',
                        title: '子流程标题'
                    }, {
                        field: 'StartDeviceName',
                        title: '启动流程设备'
                    }, {
                        field: 'ParTypeString',
                        title: '启动流程参数'
                    }, {
                        field: 'ParValue',
                        title: '启动参数'
                    }, {
                        field: 'ProcessOrderId',
                        title: '流程执行顺序'
                    },

                    ],
                    onClickRow: function (row, $element) {
                        //$('.info').removeClass('info');
                        //$($element).addClass('info');
                        //alert(row);
                        var exprocessId = row.ExProcessId;
                        LoadExProcessSteps(exprocessId);
                    }
                });
            };

            //得到查询的参数
            oTableInit.queryParams = function (params) {
                var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    limit: params.limit,   //页面大小
                    offset: params.offset,  //页码
                    ProcessId: pcode,
                    Action: "GetExprocessList"
                };
                return temp;
            };
            return oTableInit;
        };


        var ButtonInit = function () {
            var oInit = new Object();
            var postdata = {};

            oInit.Init = function () {
                //初始化页面上面的按钮事件
            };

            return oInit;
        };
    </script>

    <script>
        var setpTableInit = function (pcode) {
            var oTableInit = new Object();
            //初始化Table
            oTableInit.Init = function () {
                $('#tb_exprocesssteplist').bootstrapTable({
                    url: '/Ashx/ProcessHandler.ashx',         //请求后台的URL（*）
                    method: 'get',                      //请求方式（*）
                    toolbar: '#steptoolbar',                //工具按钮用哪个容器
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: true,                   //是否显示分页（*）
                    sortable: false,                     //是否启用排序
                    sortOrder: "asc",                   //排序方式
                    queryParams: oTableInit.queryParams,//传递参数（*）
                    sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
                    pageNumber: 1,                       //初始化加载第一页，默认第一页
                    pageSize: 10,                       //每页的记录行数（*）
                    pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                    search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
                    strictSearch: true,
                    showColumns: false,                  //是否显示所有的列(列选择器)
                    showRefresh: false,                  //是否显示刷新按钮
                    minimumCountColumns: 2,             //最少允许的列数
                    clickToSelect: true,                //是否启用点击选中行
                    height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                    uniqueId: "ExProcessId",                     //每一行的唯一标识，一般为主键列
                    showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
                    cardView: false,                    //是否显示详细视图
                    detailView: false,                   //是否显示父子表
                    singleSelect: true,                    //是否单选
                    columns: [{
                        field: 'StepTitle',
                        title: '步骤标题'
                    }, {
                        field: 'OrderIndex',
                        title: '步骤执行顺序'
                    }, {
                        field: 'ProcessDeviceName',
                        title: '工艺设备编号'
                    }, {
                        field: 'ActionTypeString',
                        title: '操作类型'
                    }, {
                        field: 'IsSync',
                        title: '下一步同步启动',
                        formatter: function (value, row, index) {
                            if (value) {
                                return "同步";
                            } else {
                                return "不同步";
                            }
                        }
                    }, {
                        field: 'SyncStepInterval',
                        title: '同步间隔(min)'
                    }, {
                        field: 'FinishParTypeString',
                        title: '完成参数类型'
                    },
                    {
                        field: 'FinishParUnit',
                        title: '完成参数单位'
                    },
                    {
                        field: 'FinishParValue',
                        title: '完成参数值'
                    },
                    {
                        field: 'BeforeStepId',
                        title: '上一步编号'
                    },
                    {
                        field: 'Delayed',
                        title: '延时量(min)'
                    }
                    ],
                    onClickRow: function (row, $element) {
                        //$('.info').removeClass('info');
                        //$($element).addClass('info');
                        //alert(row);
                    }
                });
            };
            //得到查询的参数
            oTableInit.queryParams = function (params) {
                var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    limit: params.limit,   //页面大小
                    offset: params.offset,  //页码
                    ExProcessId: pcode,
                    Action: "GetExProcessSteps"
                };
                return temp;
            };
            return oTableInit;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="sidebar">
                <!--
        Tip 1: You can change the color of the sidebar using: data-color="blue | green | orange | red"
    -->
                <div class="sidebar-wrapper">
                    <div class="logo">
                        <a href="javascript:void(0)" class="simple-text logo-mini">CT
          </a>
                        <a href="javascript:void(0)" class="simple-text logo-normal">华诺工艺
          </a>
                    </div>
                    <ul class="nav">
                        <li class="active ">
                            <a href="./Main.aspx">
                                <i class="tim-icons icon-chart-pie-36"></i>
                                <p>工艺流程</p>
                            </a>
                        </li>

                    </ul>
                </div>
            </div>
            <div class="main-panel">
                <!-- Navbar -->
                <nav class="navbar navbar-expand-lg navbar-absolute navbar-transparent">
                    <div class="container-fluid">
                        <div class="navbar-wrapper">
                            <div class="navbar-toggle d-inline">
                                <button type="button" class="navbar-toggler">
                                    <span class="navbar-toggler-bar bar1"></span>
                                    <span class="navbar-toggler-bar bar2"></span>
                                    <span class="navbar-toggler-bar bar3"></span>
                                </button>
                            </div>
                            <a class="navbar-brand" href="javascript:void(0)">Dashboard</a>
                        </div>
                        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigation" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                        </button>
                        <div class="collapse navbar-collapse" id="navigation">
                            <ul class="navbar-nav ml-auto">
                                <li class="search-bar input-group">
                                    <button class="btn btn-link" id="search-button" data-toggle="modal" data-target="#searchModal">
                                        <i class="tim-icons icon-zoom-split"></i>
                                        <span class="d-lg-none d-md-block">Search</span>
                                    </button>
                                </li>
                                <li class="dropdown nav-item">
                                    <a href="javascript:void(0)" class="dropdown-toggle nav-link" data-toggle="dropdown">
                                        <div class="notification d-none d-lg-block d-xl-block"></div>
                                        <i class="tim-icons icon-sound-wave"></i>
                                        <p class="d-lg-none">
                                            Notifications
                 
                                        </p>
                                    </a>
                                    <ul class="dropdown-menu dropdown-menu-right dropdown-navbar">
                                        <li class="nav-link"><a href="#" class="nav-item dropdown-item">Mike John responded to your email</a></li>
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">You have 5 more tasks</a></li>
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">Your friend Michael is in town</a></li>
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">Another notification</a></li>
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">Another one</a></li>
                                    </ul>
                                </li>
                                <li class="dropdown nav-item">
                                    <a href="#" class="dropdown-toggle nav-link" data-toggle="dropdown">
                                        <div class="photo">
                                            <img src="../assets/img/anime3.png" alt="Profile Photo">
                                        </div>
                                        <b class="caret d-none d-lg-block d-xl-block"></b>
                                        <p class="d-lg-none">
                                            Log out
                 
                                        </p>
                                    </a>
                                    <ul class="dropdown-menu dropdown-navbar">
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">Profile</a></li>
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">Settings</a></li>
                                        <li class="dropdown-divider"></li>
                                        <li class="nav-link"><a href="javascript:void(0)" class="nav-item dropdown-item">Log out</a></li>
                                    </ul>
                                </li>
                                <li class="separator d-lg-none"></li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div class="modal modal-search fade" id="searchModal" tabindex="-1" role="dialog" aria-labelledby="searchModal" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <input type="text" class="form-control" id="inlineFormInputGroup" placeholder="SEARCH" />
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <i class="tim-icons icon-simple-remove"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Navbar -->
                <div class="content">

                    <div class="row">

                        <div class="card-deck mb-3 text-center">

                            <%=CardList %>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-header">
                                    <h4 class="card-title">子流程列表</h4>
                                </div>
                                <div class="card-body">

                                    <div id="toolbar" class="btn-group">
                                        <button id="btn_add" type="button" class="btn btn-default" title="新增">
                                            <span class="fa fa-plus" aria-hidden="true"></span>
                                        </button>
                                        <button id="btn_edit" type="button" class="btn btn-default" title="修改">
                                            <span class="fa fa-pencil" aria-hidden="true"></span>
                                        </button>
                                        <button id="btn_delete" type="button" class="btn btn-default" title="删除">
                                            <span class="fa fa-remove" aria-hidden="true"></span>
                                        </button>
                                    </div>
                                    <table id="tb_exprocesslist"></table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-header">
                                    <h4 class="card-title">流程步骤列表</h4>
                                </div>
                                <div class="card-body">

                                    <div id="steptoolbar" class="btn-group">
                                        <button id="btn_addStep" type="button" class="btn btn-default" title="新增">
                                            <span class="fa fa-plus" aria-hidden="true"></span>
                                        </button>
                                        <button id="btn_editStep" type="button" class="btn btn-default" title="修改">
                                            <span class="fa fa-pencil" aria-hidden="true"></span>
                                        </button>
                                        <button id="btn_deleteStep" type="button" class="btn btn-default" title="删除">
                                            <span class="fa fa-remove" aria-hidden="true"></span>
                                        </button>
                                    </div>
                                    <table id="tb_exprocesssteplist"></table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
