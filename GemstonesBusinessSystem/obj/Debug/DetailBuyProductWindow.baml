   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   4    .GemstonesBusinessSystem.DetailBuyProductWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5          �Name$  detailBuyProduct��5
   	      DetailBuyProductVM � DataContext# [  5   	    � Title$" Chi tiết phiếu mua hàng��5      $	��520��6+   $	��800��68    � WindowStartupLocation$ CenterScreen=�6D    � 
ResizeMode$ CanMinimize=�6i    � 	Resources 5       �� 
TargetType �� Property �� Value��%}   '��       �� 5   
   " ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 14(�6)   66   5      5      .��5      ]� ���#2A323C��.  .+�5   
   � $	��Top=�6   $��Center=�6$   $��55��6?   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist	   ShadowDepth$	 Depth5=�6`   .+�5      �� 
 �� Padding$
 20 0 0 0q�6   $��Center=�6+   $"��Chi tiết phiếu mua hàng��6F   
�������$��25(�6x   $��DemiBold"�6�   6�   5      � 5   
   $	��Top=�6   $��20q�6$    � RowDefinitions 5      �� 5      6"   �� 5       6"    � ColumnDefinitions 5"      �� 5#      6%   �� 5$      6%   .+�5&      �� $��0��6   $��0��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5'      ��  �� Orientation$ 
Horizontal=�6   .+�5(      �� $	��120��6    $��Số phiếu��6,   6>   �� 5)      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *PhieuMuaHang.MaPhieuMuaHang+6|   5*      5+      �� 5,      $��0��6   $��1��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5-      �� $ 
Horizontal=�6   .+�5.      �� $	��120��6    $��Người tạo��6,   6?   �� 5/      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *#!PhieuMuaHang.NHANVIEN.TenNhanVien+6�   50      51      �� 52      $��1��6   $��0��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�53      �� $ 
Horizontal=�6   .+�54      �� $	��120��6    $��Nhà cung cấp��6,   6B   �� 55      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *'%PhieuMuaHang.NHACUNGCAP.TenNhaCungCap+6�   56      57      �� 58      $��1��6   $��1��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�59      �� $ 
Horizontal=�6   .+�5:      �� $	��120��6    $��Ngày tạo��6,   6>   �� 5;      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *PhieuMuaHang.NgayLapPhieuMua+ �� StringFormat$ 
dd-MM-yyyy��6�   5<      5=      5>      �� 5?   
    �� CornerRadius$ 15��6   $
��30 0q�6#   ���#323C48$	��Top=�6F   $	��230��6[   ._�5@      &   System.Windows.Controls.DataGrid  -   5          Name$ GridProduct��5@      $��20q�6.      GridLinesVisibility$
 None=�6:   $��Visible=�5A   +   $��Disabled=�6^   ���#323C48   RowDetailsVisibilityMode$ VisibleWhenSelected=�5B   @      SelectionMode$ Extended=�6o      AutoGenerateColumns .     
IsReadOnly . ��5C      �� *DSChiTietPMH+ 5D      *  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '        '  X    �� 5E      "  6   .�5F      ��  � ��$
 auto��63   �� 5G       � ��$ Center=�6>   �� 5H       � ��$ 0 5 0 5q�64   �� 5I       � �� �#98A4B96I    5J      P� 5K       �@ 
IsSelected . . �5L   "   ��  � �� �#3A45526?   �� 5M   "    � ��$ 0q�6D   6P   5N      5P      �� 5Q      "  6   .�5R      ��  � ��
 �����67   �� 5S       � �� �#56607068   �� 5T       � ��$ 0 0 0 2q�6<   �� 5U       � ��$ 50��63   �� 5V       � ��$
 auto��62   �� 5W       � ��$ 15(�65   �� 5X       � ��$ 0 0 0 0q�64   �� 5Y       � ��$
 Left=�6G   5Z      5[         RowStyle 5\      �� 5]      )  #System.Windows.Controls.DataGridRow"  6   .�5^      ��  � �� �#323C486I   �� 5_       � �� �#98A4B96I   �� 5`       � ��$ 0 0 0 1q�6<   �� 5a       � �� �#56607068   �� 5b       � ��$ Center=�6>   �� 5c       � ��$ 40��63    5d      P� 5e       �@J�IsMouseOver . . �5f   "   ��  � �� �#3944516Q   �� 5g   "    �@�Cursor$
 Hand��6;   6J   5h      P� 5i       �@ 
IsSelected . . �5j   "   ��  � �� �#3A45526Q   5k      5m         Columns 5o      0  *System.Windows.Controls.DataGridTextColumn  5p      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6)      Header$ Tên sản phẩm��64   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6K   �� *SANPHAM.TenSanPham+  5q      $ 1* 6)   $ Loại sản phẩm��64    6L   �� *$"SANPHAM.LOAISANPHAM.TenLoaiSanPham+  5r      $ 1* 6)   $ Số lượng��64    6G   �� *
SoLuongMua+  5s      $ 1* 6)   $ Đơn vị tính��64    6J   �� * SANPHAM.LOAISANPHAM.DVT.TenDVT+  5t      $ 1* 6)   $ 
Đơn giá��64    6F   �� *DonGiaSPHienTai+$ 	{0:#,##0}�� �� ConverterCulture$ vi-VN��  5u      $ 1* 6)   $ Thành tiền��64    6I   �� *	ThanhTien+$ 	{0:#,##0}��$ vi-VN��5w      5x      ]� 5y   
   $��Bottom=�6   $��	20 0 0 10q�6-   .+�5z      �� $��0��6   $��0��6"   $��0 0 0 0q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5{      �� $ Vertical=�6   .+�5|      �� $	��120��6    $��Tổng số lượng��6,   6C   �� 5}      ���#00C0EF$��18(�65   $
��Left=�6C   $��0 5 0 0q�6^   ��6o   �� *PhieuMuaHang.TongSoLuongMua+6�   5~      5      �� 5�      $��0��6   $��0��6"   $��0 0 0 0q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5�      �� $ Vertical=�6   .+�5�      �� $	��120��6    $��Tổng thành tiền��6,   6E   �� 5�      ���#00A65A$��18(�66   $
��Left=�6D   $��0 5 0 0q�6_   ��6p   �� *PhieuMuaHang.TongThanhTien+$ 	{0:#,##0}��$ vi-VN��6�   5�      5�      �� 5�      $��Right=�6   $��Right=�6-   $��
0 10 30 12q�6I   $ 10��6]   $��45��6o   .
  (System.Windows.Interactivity.Interaction 
  Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�          	EventName$  MouseLeftButtonDown��6%   .  (System.Windows.Interactivity.TriggerBase!   Actions.! 5�      6  0System.Windows.Interactivity.InvokeCommandAction  "   Command" 60   �� *XacNhanCommand+#   CommandParameter# 5�   8   �� $ �� ElementName$$ detailBuyProduct��5�      5�      ��5�      �� 5�      " ��6   .�5�      ��  � �� �#04A2B36I    5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#038B9A6Q   5�      5�      ._�5�      �� $
 	15 2 15 0q�6   
�������$��Xác nhận��6D   $��Center=�6T   $��Center=�6o   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      