   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   6    0GemstonesBusinessSystem.NewBillSellProductWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5        $��billSellProduct��5
   	   $	��700��5   	   $
��1180��6      NewBillSellProductVM  � DataContext#  [  5   	    � Title$ Phiếu bán hàng��5   	    � WindowStartupLocation$ CenterScreen=�6    .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ billSellProduct��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5      ]� ��. ���#2A323C.+�5   
   � $��60��6   $��Center=�6   $	��Top=�66   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5      �� $��Center=�6    �� Padding$ 20 0 0 0q�63   $��Phiếu bán hàng��6F   
�������$��25(�6o   $��DemiBold"�6}   6�   5      �� 5   
   ���#2A323C.��5       ]� .+�5!      � $	��Top=�6   ���#2A323C$��10,10,9.6,10q�6A   .+�5"      ]� .+�5#      � $��20 20 20 10q�6   $	��Top=�64    � ColumnDefinitions 5$      �� 5%   "   $��3* �63   6@   �� 5&   "   $��3* �63   6@   �� 5'   "   $��4* �63   6@    � RowDefinitions 5)      �� 5*   "   62   �� 5+   "   62   �� 5,   "   62   .+�5.      ��  �� CornerRadius$ 15��6%   $��0 0 30 0q�67   ���#323C48$��0��6_   $��0��6o   $��2��6|   $��3��6�   ._�5/   "   � $��30q�6'    50   &   �� 51   *   6:   �� 52   *   6:   �� 53   *   6:    55   &   �� 56   *   6=   �� 57   *   6=   .+�59   &   �� $��0��61   $��0��6A    �� Orientation$ Vertical=�6N   $��10q�6e   .+�5:   *   �� $ 
Horizontal=�65   .+�5;   .   �� $��Tên khách hàng ��68   #��[ 6O   6s   �� 5<   .   $��*��68   
���  ��#��[ 6R   6v   5=   +   �� 5>   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p    5?   .     5@   2   $ MouseLeftButtonDown��6A   . 5A   6      6L   �� *CapNhatKHCommand+5B   7   5C   3   ��5E   .   �� 5F   2   "
 ��68    �� Triggers 5G   6   P� 5H   :    P� Property �@J�IsMouseOver P� Value . . �5I   >   ��  �@�Cursor$
 Hand��6W   6f   5J   ;   5L   3   ._�5N   .   �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Name��68   $��20(�6^   
�������$��0 5 0 0q�6   ��6�   �� *TenKhachHang+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5O   /   5P   +   5Q   '   �� 5R   &   $��0 0 0 0q�6-   $	��220��6>   $��1��6J   $��Right=�6Z   $ 10��6q   $��55��6�    5S   *     5T   .   $ MouseLeftButtonDown��6=   . 5U   2      6H   �� *CapNhatKHCommand+5V   3   5W   /   ��5Y   *   �� 5Z   .   "
 ��64   .�5[   2   ��  � �� �#04A2B36a    5\   2   P� 5]   6    �@J�IsMouseOver . . �5^   :   ��  � �� �#038B9A6i   5_   7   5a   /   ._�5c   *   �� $ 	15 2 15 0q�64   
�������$��Chọn khách hàng��6\   $��Center=�6s   $��Center=�6�   $
��auto��5d   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5e   +   5f   '   �� 5h   &   $��1��61   $��1��6A   $ Vertical=�6N   $��10q�6e   .+�5i   *   �� $ 
Horizontal=�65   .+�5j   .   �� $��Ngày lập��68   #��[ 6H   6l   �� 5k   .   $��*��68   
���  ��#��[ 6R   6v   5l   +   �� 5m   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5n   .   (  "System.Windows.Controls.DatePicker  $��45��69   $��	0 -12 0 0q�6E   $	��150��6X   
�������
�������$��18(�5o   9   $
��Left=�6G    ( $MaterialDesignFloatingHintDatePicker#��[ 6b      SelectedDate 5n   x   �� *" PhieuBanHangTemp.NgayLapPhieuBan+ �� StringFormat$ 
dd-MM-yyyy�� . $ PropertyChanged=� �� NotifyOnTargetUpdated .  �� Mode$ TwoWay=�   DisplayDate 5o   �   �� *	NgayTao+$ PropertyChanged=� . 5p   /   5q   +   5r   '   �� 5s   &   $��0��61   $��1��6A   $ Vertical=�6N   $��10q�6e   .+�5t   *   �� $ 
Horizontal=�65   .+�5u   .   �� $��Số điện thoại ��68   #��[ 6N   6r   �� 5v   .   $��*��68   
���  ��#��[ 6R   6v   5w   +   �� 5x   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p    5y   .     5z   2   $ MouseLeftButtonDown��6A   . 5{   6      6L   �� *CapNhatKHCommand+5|   7   5}   3   ��5   .   �� 5�   2   "
 ��68    5�   6   P� 5�   :    �@J�IsMouseOver . . �5�   >   ��  �@�Cursor$
 Hand��6W   6f   5�   ;   5�   3   ._�5�   .   �� $��20(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *SDTKhachHang+ . $ PropertyChanged=�5�   /   5�   +   5�   '   �� 5�   &   $��0��61   $��2��6A   $ Vertical=�6N   $��10q�6e   .+�5�   *   �� $ 
Horizontal=�65   .+�5�   .   �� $��Tổng số lượng ��68   #��[ 6N   6r   �� 5�   .   $��*��68   
���  ��#��[ 6R   6v   5�   +   �� 5�   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5�   .   �� $��20(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *TongSoLuong+ . $ PropertyChanged=�5�   /   5�   +   5�   '   �� 5�   &   $��1��62   $��2��6B   $ Vertical=�6O   $��10q�6f   .+�5�   *   �� $ 
Horizontal=�65   .+�5�   .   �� $��Tổng thành tiền ��68   #��[ 6P   6t   �� 5�   .   $��*��68   
���  ��#��[ 6R   6v   5�   +   �� 5�   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5�   .   �� $��20(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *TongThanhTien+ . $ PropertyChanged=�5�   /   5�   +   5�   '   5�   #   5�      �� 5�      $��2��6%   $��3��65   $ 15��6F   ���#323C48._�5�   "   &   System.Windows.Controls.DataGrid  $��20q�6+      GridLinesVisibility$
 None=�67   $��Visible=�5�   +   $��Disabled=�6^      SelectionUnit$ FullRow=�6�   ���#323C48#  [  5�   A       CanUserAddRows  .  !   SelectionMode$! Extended=�5�   +   "   AutoGenerateColumns" .  # �� SelectedItem# 5�   +   �� *ChiTietHDDaChon+$ PropertyChanged=� . $ TwoWay=���5�   D   �� *DSSanPhamDaChon+$ PropertyChanged=� .  5�   &     5�   *   $ UnloadingRow��69   . 5�   .      6D   �� *XoaSPTuHDCommand+ 5�   E   �� $ �� Path$$ ChiTietHDDaChon�5�   /   5�   +     5�   *   $ RowEditEnding��69   . 5�   .      6D   �� *SuaSLSPTuHDCommand+ 5�   E   �� $$ ChiTietHDDaChon�5�   /   5�   +     5�   *   $ LayoutUpdated��69   . 5�   .      6D   �� *ThayDoiSLSPTuHDCommand+ 5�   E   �� $$ ChiTietHDDaChon�5�   /   5�   +   	 5�   &   *	  $System.Windows.Controls.DataGridCell=
  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '	        '
  X    �� 5�   *   "
 	 60   .�5�   .   ��  � ��$
 auto��6G   �� 5�   .    � ��$ Center=�6R   �� 5�   .    � ��$ 0 5 0 5q�6H   �� 5�   .    � �� �#98A4B96]    5�   .   P� 5�   2    �@	 
IsSelected . . �5�   6   ��  � �� �#3A45526S   �� 5�   6    � ��$ 0q�6X   6d   5�   3   5�   +   �� 5�   *   "
 
 60   .�5�   .   ��  � ��
 �����6K   �� 5�   .    � �� �#5660706L   �� 5�   .    � ��$ 0 0 0 2q�6P   �� 5�   .    � ��$ 50��6G   �� 5�   .    � ��$
 auto��6F   �� 5�   .    � ��$ 15(�6I   �� 5�   .    � ��$ 15 0 0 0q�6H   �� 5�   .    � ��$
 Left=�6[   5�   +   5�   '   %   RowStyle% 5�   &   �� 5�   *   )  #System.Windows.Controls.DataGridRow"
  60   .�5�   .   ��  � �� �#323C486]   �� 5�   .    � �� �#98A4B96]   �� 5�   .    � ��$ 0 0 0 1q�6P   �� 5�   .    � �� �#5660706L   �� 5�   .    � ��$ Center=�6R   �� 5�   .    � ��$ 40��6G    5�   .   P� 5�   2    �@J�IsMouseOver . . �5�   6   ��  � �� �#3944516e   �� 5�   6    �@�Cursor$
 Hand��6O   6^   5�   3   P� 5�   2    �@ 
IsSelected . . �5�   6   ��  � �� �#3A45526e   5�   3   5�   +   & J� InputBindings& 5�   &   d� 5�   *   ' d� Gesture$' RightDoubleClicka�67   ( �� Command( 5�   7   �� *GiamSLSPCommand+) �� CommandParameter) 5�   7   �� $$ ChiTietHDDaChon�*   Columns* 5�   &   0  *System.Windows.Controls.DataGridTextColumn  5�   *   ,  &System.Windows.Controls.DataGridColumn+   Width5  /System.Windows.Controls.DataGridLengthConverter$+ 2* 6=   ,   
IsReadOnly, . -   Header$- Sản phẩm��6Z   1  +System.Windows.Controls.DataGridBoundColumn.   Binding. 6m   �� *SANPHAM.TenSanPham+$ PropertyChanged=� . 1  +MaterialDesignThemes.Wpf.DataGridTextColumn  5�   *    5 1MaterialDesignDataGridTextColumnPopupEditingStyle/   EditingElementStyle#/ [ 6�   $- Số lượng��6  . 6L   �� *
SoLuongBan+$ PropertyChanged=� .  . 0   ElementStyle0 5�   .   �� 5�   2   "
 ��68   .�5�   6   ��  � ��$ Center=�6]   �� 5�   6    � �� �#98A4B96e   5�   3   5�   +     5�   *   $+ 1* 6=   , . $- Tổng giá��6Z   . 6m   �� *	ThanhTien+$ 	{0:#,##0}��1 �� ConverterCulture$1 vi-VN��$ PropertyChanged=� .  . 6  5�   #   5�      5�      5�      5      ]� 5     .+�5     �� $��30 0 0 0q�6    $	��Top=�62   ���#00A4FE$��35(�6\   $��Danh sách sản phẩm��6k   6�   �� 5     $
��auto��6   $	��600��6+   $ 15��6;   $	��Top=�6M   $��30 30 30 10q�6b   ���#323C48._�5     �� $��Visible=�6'   $��Disabled=�6M   $��0 0 0 0q�6v   .��5     �� $
��0 20q�6+   #  [  69   ���#404750$��1 0 1 1q�6�   ���#323C48��5  8   �� *	DSSanPham+ . $ PropertyChanged=���5  "   �� 5  &   .R�5	  *   � 2 � Orientation$2 
Horizontal=�65   6P   5
  '   ��5  "   �� 5  &   .R�5  *   �� $	��160��65   $
��auto��6A   $��10q�6O   ��5  .   �� 5  2   "
 ��68   .�5  6   ��  � �� 6S   Z� 6f    5  6   P� 5  :    �@J�IsMouseOver . . �5  >   ��  � �� �#3944516m   �� 5  >    �@�Cursor$
 Hand��6W   6f   5  ;   5  3   .+�5  .   �� $	��120��64   $	��120��6A   $��0 10 0 0q�6j   ��6M   �� *	HinhAnhSP+ 5  2     5  6   $ MouseLeftButtonDown��6E   . 5  :      6P   �� 3 �� RelativeSource3 �� 4 �� AncestorType"4 ��$$ DataContext.ThemSPVaoHD� 5  T   �� $$ 	MaSanPham�5  ;   5   7   5"  /   �� 5#  .   
�������$��20(�6K   $��Light"�6u   $��0 5 0 0q�6�   $
��Wrap=�6�   $��Center=�6�   5 �� TextAlignment$5 Center=�6�   $��Center=�6�   ��6Y   �� *
TenSanPham+ 5$  C     5%  G   $ MouseLeftButtonDown��6V   . 5&  K      6a   �� 3 �� "4 ��$$ DataContext.ThemSPVaoHD� 5'  T   �� $$ 	MaSanPham�5(  L   5)  H   5+  /   �� 5,  .   ���#C48C46$��17(�6M   $��Light"�6�   $��0 5 0 0q�6�   $
��Wrap=�6�   $��Center=�6�   $5 Center=�6�   $��Center=�6  ��6[   �� *	DonGiaBan+$ 	{0:#,##0}��$1 vi-VN��5-  /   �� 5.  .   $ 
Horizontal=�69   $��Center=�6R    5/  2     50  6   $ MouseLeftButtonDown��6E   . 51  :      6P   �� 3 �� "4 ��$$ DataContext.ThemSPVaoHD� 52  T   �� $$ 	MaSanPham�53  ;   54  7   .+�56  2   �� ���#C48C46$��13(�6Q   $��Số lượng: ��6_   $��Light"�6q   $��0 0 0 0q�6�   $
��Wrap=�6�   $��Center=�6�   $5 Center=�6�   $��Center=�6�   57  3   �� 58  2   ���#C48C46$��13(�6Q   $��Light"�59  L   $��0 0 0 0q�6_   $
��Wrap=�6p   $��Center=�6�   $5 Center=�6�   $��Center=�6�   ��58  _   �� *TongSoLuongTon+$ PropertyChanged=� . 5:  3   5;  /   5<  +   5=  '   5?     5@     5A     ]� 5B     $
��0 10q�6    $	��Top=�6.   ��.  .+�5C     �� $��0 0 30 0q�6"   $��Right=�64   $ 10��6K   $��45��6]    5D       5E  "   $ MouseLeftButtonDown��61   . 5F  &      6<   �� *HuyBoCommand+ 5G  8   �� $ billSellProduct��5H  '   5I  #   ��5K     �� 5L  "   "
 ��6(   .�5M  &   ��  � �� �#F8F9FA6U    5N  &   P� 5O  *    �@J�IsMouseOver . . �5P  .   ��  � �� �#E2E6EA6]   5Q  +   5S  #   ._�5U     �� $ 	15 2 15 0q�6(   ���#212529$��
Hủy bỏ��6R   $��Center=�6`   $��Center=�6{   $
��auto��5V  +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5W     5X     �� 5Y     $��0 0 10 0q�6"   $��Right=�64   $ 10��6K   $��45��6]    5Z       5[  "   $ MouseLeftButtonDown��61   . 5\  &      6<   �� *XacNhanCommand+ 5]  8   �� $ billSellProduct��5^  '   5_  #   ��5a     �� 5b  "   "
 ��6(   .�5c  &   ��  � �� �#04A2B36U    5d  &   P� 5e  *    �@J�IsMouseOver . . �5f  .   ��  � �� �#038B9A6]   5g  +   5i  #   ._�5k     �� $ 	15 2 15 0q�6(   
�������$��Xác nhận��6P   $��Center=�6`   $��Center=�6{   $
��auto��5l  +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5m     5n     5o     5q     5r     5s     5t     5u     