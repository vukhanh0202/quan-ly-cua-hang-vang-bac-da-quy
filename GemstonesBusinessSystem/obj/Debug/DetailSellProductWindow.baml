   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   5    /GemstonesBusinessSystem.DetailSellProductWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5          �Name$  detailSellProduct��5   	      DetailSellProductVM � DataContext# [  5	   	    � Title$# Chi tiết phiếu bán hàng��5   	   $	��520��6)   $	��800��66    � WindowStartupLocation$ CenterScreen=�6B    � 
ResizeMode$ CanMinimize=�6g   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+	   CommandParameter	 5   $   �� 
 �� ElementName$
 detailSellProduct��5       � 	Resources 5       �� 
TargetType �� Property �� Value��%}   '��       �� 5   
   " ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 14(�6)   66   5      5      .��5      ]� ���#2A323C��.  .+�5   
   � $	��Top=�6   $��Center=�6$   $��60��6?   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5      ��  �� Padding$ 20 0 0 0q�6   $��Center=�6+   $#��Chi tiết phiếu bán hàng��6F   
�������$��25(�6x   $��DemiBold"�6�   6�   5      � 5   
   $	��Top=�6   $��20q�6$    � RowDefinitions 5       �� 5!      6"   �� 5"      6"    � ColumnDefinitions 5$      �� 5%      6%   �� 5&      6%   .+�5(      �� $��0��6   $��0��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5)      ��  �� Orientation$ 
Horizontal=�6   .+�5*      �� $	��120��6    $��Số phiếu��6,   6>   �� 5+      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *PhieuBanHang.MaPhieuBanHang+6|   5,      5-      �� 5.      $��0��6   $��1��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5/      �� $ 
Horizontal=�6   .+�50      �� $	��120��6    $��Người tạo��6,   6?   �� 51      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *#!PhieuBanHang.NHANVIEN.TenNhanVien+6�   52      53      �� 54      $��1��6   $��0��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�55      �� $ 
Horizontal=�6   .+�56      �� $	��120��6    $��Khách hàng��6,   6@   �� 57      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *%#PhieuBanHang.KHACHHANG.TenKhachHang+6�   58      59      �� 5:      $��1��6   $��1��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5;      �� $ 
Horizontal=�6   .+�5<      �� $	��120��6    $��Ngày tạo��6,   6>   �� 5=      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *PhieuBanHang.NgayLapPhieuBan+ �� StringFormat$ 
dd-MM-yyyy��6�   5>      5?      5@      �� 5A   
    �� CornerRadius$ 15��6   $
��30 0q�6#   ���#323C48$	��Top=�6F   $	��230��6[   ._�5B      &   System.Windows.Controls.DataGrid  -   5          Name$ GridProduct��5B      $��20q�6.      GridLinesVisibility$
 None=�6:   $��Visible=�5C   +   $��Disabled=�6^   ���#323C48   RowDetailsVisibilityMode$ VisibleWhenSelected=�5D   @      SelectionMode$ Extended=�6o      AutoGenerateColumns .     
IsReadOnly . ��5E      �� *DSChiTietPBH+ 5F      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels �� Triggers  P� Property! P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%q  '        '      �� 5G      "  6   .�5H      ��  � ��$ 0 0 0 1q�6<   �� 5I       � �� �#56607068   �� 5J       � ��$ 30��63   �� 5K       � ��$ Center=�6>   �� 5L       � ��$ 	10 10 0 0q�64   �� 5M       � �� �#98A4B96I   �� 5N       � �� 5O      �� 5P   "   "  62   .R�5Q   &   �� # z��6-   #��z��6Q   #��z��6}   #��z��6�    . ._�5R   *   �� # z 6;   ��5S   .   �� 5T   2   .R�5U   6   �� $��texte��6Y   
������ $��CharacterEllipsis=�6f   $
��auto��5V   P   $
��auto��6^   $��Center=�6k   ��6�   �� *Text+5W   3   5Y   +   5Z   '   5[   #   5]       5^      P� 5_        �@ 
IsSelected! . . �5`   "   ��  � �� �#3A45526?   5a      5c      �� 5d      "  6   .�5e      ��  � ��
 �����67   �� 5f       � �� �#56607068   �� 5g       � ��$ 0 0 0 1q�6<   �� 5h       � ��$ 40��63   �� 5i       � ��$
 auto��62   �� 5j       � ��$ 13(�65   �� 5k       � ��$ 15 0 0 0q�64   �� 5l       � ��$
 Left=�6G   5m      5n      "   RowStyle" 5o      �� 5p      )	  #System.Windows.Controls.DataGridRow" 	 6   .�5q      ��  � �� �#323C486I   �� 5r       � �� �#98A4B96I   �� 5s       � �� �#56607068   �� 5t       � ��$ 0q�6<   �� 5u       � ��$	 5 0q�63   �� 5v       � ��$ 5 10 0 0q�64   �� 5w       � ��$ Center=�6>    5x      P� 5y        �@J�IsMouseOver! . . �5z   "   ��  � �� �#3944516Q   �� 5{   "    �@�Cursor$
 Hand��6;   6J   5|      P� 5}        �@	 
IsSelected! . . �5~   "   ��  � �� �#3A45526Q   5      5�      #   Columns# 5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�      ,  &System.Windows.Controls.DataGridColumn$   Width5  /System.Windows.Controls.DataGridLengthConverter$$ 1* 6)   %   Header$% Tên sản phẩm��64   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6K   �� *SANPHAM.TenSanPham+
  5�      $$ 1* 6)   $% Loại sản phẩm��64   & 6L   �� *$"SANPHAM.LOAISANPHAM.TenLoaiSanPham+
  5�      $$ 1* 6)   $% Số lượng��64   & 6G   �� *
SoLuongBan+
  5�      $$ 1* 6)   $% Đơn vị tính��64   & 6J   �� * SANPHAM.LOAISANPHAM.DVT.TenDVT+
  5�      $$ 1* 6)   $% 
Đơn giá��64   & 6F   �� *DonGiaSPHienTai+$ 	{0:#,##0}��' �� ConverterCulture$' vi-VN��
  5�      $$ 1* 6)   $% Thành tiền��64   & 6I   �� *	ThanhTien+$ 	{0:#,##0}��$' vi-VN��5�      5�      ]� 5�   
   $��Bottom=�6   $��	20 0 0 10q�6-   .+�5�      �� $��0��6   $��0��6"   $��0 0 0 0q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5�      �� $ Vertical=�6   .+�5�      �� $	��120��6    $��Tổng số lượng��6,   6C   �� 5�      ���#00C0EF$��18(�65   $
��Left=�6C   $��0 5 0 0q�6^   ��6o   �� *PhieuBanHang.TongSoLuongBan+6�   5�      5�      �� 5�      $��0��6   $��0��6"   $��0 0 0 0q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5�      �� $ Vertical=�6   .+�5�      �� $	��120��6    $��Tổng thành tiền��6,   6E   �� 5�      ���#00A65A$��18(�66   $
��Left=�6D   $��0 5 0 0q�6_   ��6p   �� *PhieuBanHang.TongThanhTien+$ 	{0:#,##0}��$' vi-VN��6�   5�      5�      �� 5�      $��Right=�6   $��Right=�6-   $��
0 10 30 12q�6I   $ 10��6]   $��45��6o    5�        5�      $ MouseLeftButtonDown��6!   . 5�         60   �� *XacNhanCommand+	 5�   8   �� $
 detailSellProduct��5�      5�      ��5�      �� 5�      " ��6   .�5�      ��  � �� �#04A2B36E    5�      P� 5�        �@J�IsMouseOver! . . �5�      ��  � �� �#038B9A6M   5�      5�      ._�5�      �� $ 	15 2 15 0q�6   
�������$��Xác nhận��6@   $��Center=�6P   $��Center=�6k   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      