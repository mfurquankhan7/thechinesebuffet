<?PHP
    include('connection.php');
    include('userdetails.php');
    $main = 1;
    $sub = 2;
    if(isset($_COOKIE['locationId'])){
        $locationId = $_COOKIE['locationId'];
    }
?>
    <!DOCTYPE html>
    <html>

    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>The Chinese Buffet - Menu</title>
        <!-- Tell the browser to be responsive to screen width -->
        <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">

        <!-- Bootstrap 3.3.5 -->
        <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
        <!-- Font Awesome -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
        <!-- Ionicons -->
        <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
        <!-- DataTables -->
        <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css">

        <link rel="stylesheet" href="plugins/select2/select2.min.css">
        <!-- Theme style -->
        <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
        <link rel="stylesheet" href="dist/css/habibhospital.css">
        <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
        <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    </head>


    <body class="hold-transition skin-blue sidebar-mini">
        <div class="wrapper">
            <header class="main-header">
                <!-- Logo -->
                <a href="index2.html" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                     <span class="logo-mini"><b>C</b>B</span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><b>Admin</b>CB</span>
                </a>
                <!-- Header Navbar: style can be found in header.less -->
                <nav class="navbar navbar-static-top" role="navigation">
                    <!-- Sidebar toggle button-->
                    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">
                            <!-- User Account: style can be found in dropdown.less -->
                            <?PHP include('sources/user-details-right.php'); ?>
                        </ul>
                    </div>
                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                    <!-- Sidebar user panel -->
                    <?PHP include('sources/user-details-left.php'); ?>
                        <!-- sidebar menu: : style can be found in sidebar.less -->
                        <ul class="sidebar-menu">
                            <li class="header">MAIN NAVIGATION</li>
                            <?PHP include('sources/main-menu.php'); ?>
                        </ul>
                </section>
                <!-- /.sidebar -->
            </aside>
            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                    Items
                    <small>View Items</small>
                </h1>
                    <ol class="breadcrumb">
                        <?PHP include('sources/breadcrum.php'); ?>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">Item List</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="form-group col-sm-3">
                                    <label>Select Location</label>
                                    <select class="form-control select2 location" name="location-name" style="width: 100%;" required>
                                        <option value="">Select Location</option>
                                        <?PHP include('sources/getLocation.php'); ?>
                                    </select>
                                </div>
                                <div class="box-body">
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>Item Name</th>
                                                <th>Delete</th>
                                            </tr>
                                        </thead>
                                        <tbody id="menu-list">
                                            <?PHP include('sources/getMenuList.php'); ?>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>Item Name</th>
                                                <th>Delete</th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                                <!-- /.box-body -->
                            </div>
                            <!-- /.box -->
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </section>
                <!-- /.content -->
            </div>
            <!-- /.content-wrapper -->
            <footer class="main-footer">
                <?PHP include('sources/footer.php'); ?>
            </footer>
        </div>
        <!-- ./wrapper -->

        <!-- jQuery 2.1.4 -->
        <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
        <!-- Bootstrap 3.3.5 -->
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <!-- Select2 -->
        <script src="plugins/select2/select2.full.min.js"></script>
        <!-- DataTables -->
        <script src="plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>
        <!-- SlimScroll -->
        <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
        <!-- FastClick -->
        <script src="plugins/fastclick/fastclick.min.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/app.min.js"></script>
        <!-- AdminLTE for demo purposes -->
        <script src="dist/js/demo.js"></script>
        <script src="dist/js/jquery.cookies.js"></script>
        
        <!-- page script -->
        <script>
            $(document).ready(function(e) {
                bindpagination();
                allFunction();
            });

            $(function() {
                $("#example1").DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": false,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            });


            function deleteMenu(e) {
                $.ajax({
                    type: "POST",
                    url: "sources/deleteitem.php?menuId=" + e,
                    success: function(data) {
                        if (data == 1) {
                            alert('Record deleted...!');
                            location.reload();
                        }
                    }
                });
                return false;
            }



            //Binding
            function bindpagination() {
                $('.pagination').find('li').bind('click', function() {
                    $(".edit-details").unbind("click");
                    $('.delete-details').unbind("click");
                    allFunction();
                    bindpagination();
                });
            }

            function allFunction() {
                $('.location').change(function() {
                    $.cookie('locationId', parseInt($(this).val()));
                    location.reload();
                });
                $('.edit').click(function() {
                    var arr = $(this).attr('id').split('-');
                    $.cookie('itemId', parseInt(arr[1]));
                    window.open('update-items.php', '_self');
                    return false;
                });
                $('.delete').click(function() {
                    var arr = $(this).attr('id').split('-');
                    deleteMenu(parseInt(arr[1]));
                });
            }

        </script>
    </body>

    </html>
