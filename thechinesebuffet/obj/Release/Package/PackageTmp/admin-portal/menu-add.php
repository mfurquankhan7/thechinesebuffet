<?PHP
    include('connection.php');
    include('userdetails.php');
    $main = 1;
    $sub = 1;
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
        <!-- daterange picker -->
        <link rel="stylesheet" href="plugins/datepicker/datepicker3.css">
        <!-- iCheck for checkboxes and radio inputs -->
        <link rel="stylesheet" href="plugins/iCheck/all.css">

        <link rel="stylesheet" href="plugins/select2/select2.min.css">
        <!-- Theme style -->
        <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
        <link rel="stylesheet" href="dist/css/habibhospital.css">

        <!-- AdminLTE Skins. Choose a skin from the css/skins folder instead of downloading all of them to reduce the load. -->
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
                    <h1>Items
                    </h1>
                    <ol class="breadcrumb">
                        <?PHP include('sources/breadcrum.php'); ?>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <!-- Main row -->
                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-8">
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Content</h3>
                                </div>
                                <!-- /.box-header -->
                                <!-- form start -->
                                <form id="uploadForm" action="sources/item-upload.php" method="post">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label>Select Location</label>
                                            <select class="form-control select2 location" name="location-name" style="width: 100%;" required>
                                                <option value="">Select Location</option>
                                                <?PHP include('sources/getLocation.php'); ?>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label>Select Location</label>
                                            <select class="form-control select2 menu" name="menu-name" style="width: 100%;" required>
                                                <option value="">Select Menu</option>
                                                
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputFile">Item Name</label>
                                            <input type="text" class="form-control" name="itemname" required>
                                        </div>
                                        
                                        <div class="progress active">
                                            <div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" id="progress-bar" style="width: 0%; height:20px;"></div>
                                        </div>
                                        <div class="form-group">
                                            <input type="hidden" name="foldername" id="foldername" value="" />
                                            <input type="hidden" name="brandNo" id="brandNo" value="" />
                                            <input type="hidden" name="categoryNo" id="categoryNo" value="" />
                                            <button type="submit" class="btn btn-primary">Submit</button>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        Design &amp; Developed By <a href="https://nativeasia.com">NativeAsia</a>.
                                    </div>
                                </form>
                            </div>
                            <!-- /.box -->
                        </div>
                        <!--/.col (left) -->
                    </div>
                    <!-- /.row (main row) -->
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
        <!-- date-range-picker -->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.2/moment.min.js"></script>
        <script src="plugins/datepicker/bootstrap-datepicker.js"></script>

        <!-- SlimScroll 1.3.0 -->
        <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
        <!-- iCheck 1.0.1 -->
        <script src="plugins/iCheck/icheck.min.js"></script>
        <!-- FastClick -->
        <script src="plugins/fastclick/fastclick.min.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/app.min.js"></script>
        <!-- AdminLTE for demo purposes -->
        <script src="dist/js/demo.js"></script>
        <script src="dist/js/jquery.form.min.js"></script>

        <!-- Page script -->
        <script>
             $(document).ready(function(e) {
                
            });
            
            $(function() {
                //Initialize Select2 Elements
                $(".select2").select2();

                //Form
                $('#uploadForm').submit(function(e) {
                    e.preventDefault();
                    $(this).ajaxSubmit({
                        beforeSubmit: function() {
                            $("#progress-bar").width('0%');
                        },
                        uploadProgress: function(event, position, total, percentComplete) {
                            $("#progress-bar").width(percentComplete + '%');
                            $("#progress-bar").html('<div id="progress-status">' + percentComplete + ' %</div>');
                        },
                        success: function(data) {
                            if (data == 1) {
                                $("#progress-bar").width('100%');
                                $("#progress-bar").html('<div id="progress-status">100 %</div>');
                                alert('Data updated...!');
                                location.reload();
                            } else {
                                alert(data);
                            }
                        },
                        resetForm: false
                    });
                    //return false;
                });
                
                $('.location').change(function() {
                    var locId = parseInt($(this).val());
                     $.ajax({
                        type: "POST",
                        url: "sources/getMenus.php?id=" + locId,
                        success: function(data) {
                            $('.menu').html(data);
                        }
                    });
                    return false;
                });
            });
            

        </script>
    </body>

    </html>
