   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   5    /GemstonesBusinessSystem.DetailBillServiceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5        $��detailBillService��5
   	      DetailBillServiceVM  � DataContext#  [  5   	    � Title$$ Chi tiết phiếu dịch vụ��5   	   $	��700��6(   $
��1150��65    � WindowStartupLocation$ CenterScreen=�6B   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ detailBillService��5      	 � 	Resources	 5      
 �� 
TargetType �� Property �� Value��%}   '��       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 18(�6)   66   5      5      .��5      ]� ���#2A323C��.  .+�5   
   � $	��Top=�6   $��Center=�6$   $��60��6?   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5      ��  �� Padding$ 20 0 0 0q�6   $��Center=�6+   $$��Chi tiết phiếu dịch vụ��6F   
�������$��25(�6w   $��DemiBold"�6�   6�   5      � 5   
   $	��Top=�6   $��20q�6$    � RowDefinitions 5      �� 5      6"   �� 5       6"    � ColumnDefinitions 5"      �� 5#      6%   �� 5$      6%   .+�5&      �� $��0��6   $��0��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5'      ��  �� Orientation$ 
Horizontal=�6   .+�5(      �� $	��120��6    $��Số phiếu��6,   6>   �� 5)      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *PhieuDichVu.MaPhieuDichVu+6z   5*      5+      �� 5,      $��0��6   $��1��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�5-      �� $ 
Horizontal=�6   .+�5.      �� $	��120��6    $��Người tạo��6,   6?   �� 5/      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *" PhieuDichVu.NHANVIEN.TenNhanVien+6�   50      51      �� 52      $��1��6   $��0��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�53      �� $ 
Horizontal=�6   .+�54      �� $	��120��6    $��Khách hàng��6,   6@   �� 55      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� *$"PhieuDichVu.KHACHHANG.TenKhachHang+6�   56      57      �� 58      $��1��6   $��1��6"   $��0 0 0 1q�62   ���#5A6576$��
10 0 10 10q�6b   ._�59      �� $ 
Horizontal=�6   .+�5:      �� $	��120��6    $��Ngày tạo��6,   6>   �� 5;      $��Right=�6    $��0 5 0 0q�6<   ��6M   �� * PhieuDichVu.NgayLapPhieuDichVu+ �� StringFormat$ 
dd-MM-yyyy��6�   5<      5=      5>      �� 5?   
    �� CornerRadius$ 15��6   $
��30 0q�6#   ���#323C48$	��Top=�6F   $	��330��6[   ._�5@      &   System.Windows.Controls.DataGrid  -   5          Name$ detailBillServiceGrid��5@      $��20q�66      GridLinesVisibility$
 None=�6B   $��Visible=�5A      $��Disabled=�6J      SelectionUnit$ FullRow=�6�   ���#323C48#  [  5B   -      CanUserAddRows .     SelectionMode$ Extended=�5C         AutoGenerateColumns .  ��60   �� *DSChiTietPDV+ �� UpdateSourceTrigger$ PropertyChanged=� �� NotifyOnSourceUpdated .  5D        5E      $ SelectionChanged��6%   . 5F         60   �� *SelectionChangedCommand+ 5G   D   �� *SelectedItems+$ detailBillServiceGrid��5H      5I      	 5K      *  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '        '  X    �� 5L      "
  6   .�5M      ��  � ��$
 auto��63   �� 5N       � ��$ Center=�6>   �� 5O       � ��$ 0 5 0 5q�64   �� 5P       � �� �#98A4B96I    5Q      P� 5R       �@ 
IsSelected . . �5S   "   ��  � �� �#3A45526?   �� 5T   "    � ��$ 0q�6D   6P   5U      5W      �� 5X      "
  6   .�5Y      ��  � ��
 �����67   �� 5Z       � �� �#56607068   �� 5[       � ��$ 0 0 0 2q�6<   �� 5\       � ��$ 50��63   �� 5]       � ��$
 auto��62   �� 5^       � ��$ 17(�65   �� 5_       � ��$ 15 0 0 0q�64   �� 5`       � ��$
 Left=�6G   5a      5b         RowStyle 5c      �� 5d      )	  #System.Windows.Controls.DataGridRow"
 	 6   .�5e      ��  � �� �#323C486I   �� 5f       � �� �#98A4B96I   �� 5g       � ��$ 0 0 0 1q�6<   �� 5h       � �� �#56607068   �� 5i       � ��$ Center=�6>   �� 5j       � ��$ 40��63    5k      P� 5l       �@J�IsMouseOver . . �5m   "   ��  � �� �#3944516Q   �� 5n   "    �@�Cursor$
 Hand��6;   6J   5o      P� 5p       �@	 
IsSelected . . �5q   "   ��  � �� �#3A45526Q   5r      5t          Columns  5w      0
  *System.Windows.Controls.DataGridTextColumn
  5x      ,  &System.Windows.Controls.DataGridColumn!   Width5  /System.Windows.Controls.DataGridLengthConverter$! 1* 6)   "   
IsReadOnly" . #   Header$# Dịch vụ��6F   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 6X   �� *DICHVU.TenDichVu+$ PropertyChanged=� . 
  5y      $! 1* 6)   " . $# Đơn giá DV��6F   $ 6[   �� *DonGiaDichVu+$ 	{0:#,##0}��% �� ConverterCulture$% vi-VN��$ PropertyChanged=� . & �� NotifyOnTargetUpdated& . 6  
  5z      $! 1* 6)   " . $# Chi phí riêng��6F   $ 6^   �� *ChiPhiRieng+$ 	{0:#,##0}��$% vi-VN��$ PropertyChanged=� . & . 6  
  5{      $! 1* 6)   " . $# Đơn giá tính��6F   $ 6]   �� *DonGiaDuocTinh+$ 	{0:#,##0}��$% vi-VN��$ PropertyChanged=� . & . 6  
  5|      $! 1* 6)   " . $# Số lượng��6F   $ 6Y   �� *SoLuongDichVu+$ PropertyChanged=� . & . 6�   
  5}      $! 1* 6)   " . $# Thành tiền��6F   $ 6[   �� *	ThanhTien+$ 	{0:#,##0}��$% vi-VN��$ PropertyChanged=� . & . 6  
  5~      $! 1* 6)   " . $# Trả trước��6F   $ 6Z   �� *TienTraTruoc+$ 	{0:#,##0}��$% vi-VN��$ PropertyChanged=� . & . 6  
  5      $! 1* 6)   " . $# 
Còn lại��6F   $ 6X   �� *
TienConlai+$ 	{0:#,##0}��$% vi-VN��$ PropertyChanged=� . & . 6	  
  5�      $! 1* 6)   " . $# NgayGiao��6F   $ 6Y   �� *
NgayGiao+$ 
dd-MM-yyyy��$ PropertyChanged=� . & . 6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $# Trạng thái��6-   $! 1* 6A   '   CellTemplate' 5�      �� 5�      .R�5�   "   �� ��5�   &   �� 5�   *   "
 ��60    5�   .   �� 5�   2   ( �� Binding( 6>   �� ) �� Path$) TinhTrangCT_PDV�* �� Value* 16h   .a�5�   6   ��  � ��$ Hoàn thành��6M   6b   5�   3   �� 5�   2   ( 6>   �� $) TinhTrangCT_PDV�* 06h   .a�5�   6   ��  � ��$ Chưa hoàn thành��6M   6g   5�   3   5�   +   5�   #   5�      5�      5�      5�      ]� 5�   
   $��Bottom=�6   $��	20 0 0 10q�6-   .+�5�      �  5�      �� 5�      6&   �� 5�      6&   .+�5�      ]� $��0��6   ��.  .+�5�      �� $��0��6   $��0 0 0 0q�6-   ���#5A6576$��
10 0 10 10q�6]   ._�5�      �� $ Vertical=�6%   .+�5�      �� $	��150��6(   $��Tổng số lượng��64   6K   �� 5�      ���#00C0EF$��18(�6=   $
��Left=�6K   $��0 5 0 0q�6f   ��6w   �� *PhieuDichVu.TongSoLuongDichVu+6�   5�      5�      �� 5�      $��0��6   $��0 0 0 0q�6-   ���#5A6576$��
10 0 10 10q�6]   ._�5�      �� $ Vertical=�6%   .+�5�      �� $	��150��6(   $��Tình trạng��64   6H   �� 5�      ���#00C0EF$��18(�6=   $
��Left=�6K   $��0 5 0 0q�6f   ��5�   "   �� 5�   &   "
 ��6,    5�   *   �� 5�   .   ( 6:   �� $) PhieuDichVu.TinhTrangPDV�* 16m   .a�5�   2   ��  � ��$ Hoàn thành��6I   6^   5�   /   �� 5�   .   ( 6:   �� $) PhieuDichVu.TinhTrangPDV�* 06m   .a�5�   2   ��  � ��$ Chưa hoàn thành��6I   6c   5�   /   5�   '   5�      5�      5�      5�      ]� 5�      $��1��6   ��.  .+�5�      �� $��0��6   $��0��6*   $��0 0 0 0q�6:   ���#5A6576$��
10 0 10 10q�6j   ._�5�      �� $ Vertical=�6%   .+�5�      �� $	��150��6(   $��Tổng thành tiền��64   6M   �� 5�      ���#00A65A$��18(�6>   $
��Left=�6L   $��0 5 0 0q�6g   ��6x   �� *PhieuDichVu.TongThanhTien+$ 	{0:#,##0}��$% vi-VN��6�   5�      5�      �� 5�      $��0��6   $��0��6*   $��0 0 0 0q�6:   ���#5A6576$��
10 0 10 10q�6j   ._�5�      �� $ Vertical=�6%   .+�5�      �� $	��150��6(   $"��Tổng tiền trả trước��64   6Q   �� 5�      ���#00A65A$��18(�6>   $
��Left=�6L   $��0 5 0 0q�6g   ��6x   �� *PhieuDichVu.TongTienTraTruoc+$ 	{0:#,##0}��$% vi-VN��6�   5�      5�      �� 5�      $��0��6   $��0��6*   $��0 0 0 0q�6:   ���#5A6576$��
10 0 10 10q�6j   ._�5�      �� $ Vertical=�6%   .+�5�      �� $	��150��6(   $��Tổng tiền còn lại��64   6O   �� 5�      ���#00A65A$��18(�6>   $
��Left=�6L   $��0 5 0 0q�6g   ��6x   �� *PhieuDichVu.TongTienConLai+$ 	{0:#,##0}��$% vi-VN��6�   5�      5�      5�      �� 5�      $��Right=�6   $��2��60   $��Right=�6B   $��0 10 180 12q�6^   $ 10��6s   $��45��6�    5�        5�      $ MouseLeftButtonDown��6)   . 5�         64   �� *HoanThanhCommand+ 5�   8   �� $ detailBillService��5�      5�      ��5�      �� 5�      "
 ��6    .�5�      ��  � �� �#04A2B36M    5�      P� 5�   "    �@J�IsMouseOver . . �5�   &   ��  � �� �#038B9A6U   5�   #   5�      ._�5�      �� $ 	15 2 15 0q�6    
�������$��Hoàn thành��6H   $��Center=�6Z   $��Center=�6u   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      �� 5�      $��Right=�6   $��2��60   $��Right=�6B   $��
0 10 30 12q�6^   $ 10��6r   $��45��6�    5�        5�      $ MouseLeftButtonDown��6)   . 5�         64   �� *XacNhanCommand+ 5�   8   �� $ detailBillService��5�      5�      ��5�      �� 5�      "
 ��6    .�5�      ��  � �� �#04A2B36M    5�      P� 5�   "    �@J�IsMouseOver . . �5�   &   ��  � �� �#038B9A6U   5�   #   5�      ._�5�      �� $ 	15 2 15 0q�6    
�������$��Xác nhận��6H   $��Center=�6X   $��Center=�6s   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      5�      