   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   2    ,GemstonesBusinessSystem.NewBillServiceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5        $��billService��5
   	      NewBillServiceVM  � DataContext#  [  5   	    � WindowStartupLocation$ CenterScreen=�69    � Title$ Thêm phiếu dịch vụ��5   	   $	��700��6$   $
��1180��61   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ billService��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5      ]� ��. ���#2A323C.+�5    
   � $��60��6   $��Center=�6   $	��Top=�66   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5!      �� $��Center=�6    �� Padding$ 20 0 0 0q�63   $��Phiếu dịch vụ��6F   
�������$��25(�6n   $��DemiBold"�6|   6�   5"      �� 5#   
   ���#2A323C.��5$      ]� .+�5%      � $	��Top=�6   ���#2A323C$��10,10,9.6,10q�6A   .+�5&      ]� .+�5'      � $��20 20 20 10q�6   $	��Top=�64    � ColumnDefinitions 5(      �� 5)   "   65   �� 5*   "   65   �� 5+   "   65    � RowDefinitions 5-      �� 5.   "   62   �� 5/   "   62   �� 50   "   62   .+�52      ��  �� CornerRadius$ 15��6%   ���#323C48$��0��6M   $��0��6]   $��2��6j   $��3��6~   ._�53   "   � $��30q�6'    54   &   �� 55   *   6:   �� 56   *   6:   �� 57   *   6:    59   &   �� 5:   *   6=   �� 5;   *   6=   .+�5=   &   �� $��0��61   $��0��6A    �� Orientation$ Vertical=�6N   $��10q�6e   .+�5>   *   �� $ 
Horizontal=�65   .+�5?   .   �� $��Tên khách hàng ��68   #��[ 6O   6s   �� 5@   .   $��*��68   
���  ��#��[ 6R   6v   5A   +   �� 5B   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p    5C   .     5D   2   $ MouseLeftButtonDown��6A   . 5E   6      6L   �� *CapNhatKHCommand+ 5F   8   �� $ BillService��5G   7   5H   3   ��5J   .   �� 5K   2   "
 ��68    �� Triggers 5L   6   P� 5M   :    P� Property �@J�IsMouseOver P� Value . . �5N   >   ��  �@�Cursor$
 Hand��6W   6f   5O   ;   5Q   3   ._�5S   .   �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Name��68   $��20(�6^   
�������$��0 5 0 0q�6   ��6�   �� *TenKhachHang+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5T   /   5U   +   5V   '   �� 5W   &   $��1��61   $��0��6A   $ Vertical=�6N   $��10q�6e   .+�5X   *   �� $ 
Horizontal=�65   .+�5Y   .   �� $��Ngày lập��68   #��[ 6H   6l   �� 5Z   .   $��*��68   
���  ��#��[ 6R   6v   5[   +   �� 5\   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5]   .   (  "System.Windows.Controls.DatePicker  $��45��69   $��	0 -12 0 0q�6E   $	��150��6X   
�������
�������$��18(�5^   9   $
��Left=�6G    ( $MaterialDesignFloatingHintDatePicker#��[ 6b      SelectedDate 5]   x   �� *#!PhieuDichVuMoi.NgayLapPhieuDichVu+ �� StringFormat$ 
dd-MM-yyyy�� . $ PropertyChanged=� �� NotifyOnTargetUpdated .  �� Mode$ TwoWay=�   DisplayDate 5^   �   �� *	NgayTao+$ PropertyChanged=� . 5_   /   5`   +   5a   '   �� 5b   &   $��0��61   $��1��6A   $ Vertical=�6N   $��10q�6e   .+�5c   *   �� $ 
Horizontal=�65   .+�5d   .   �� $��Số điện thoại ��68   #��[ 6N   6r   5e   +   �� 5f   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p    5g   .     5h   2   $ MouseLeftButtonDown��6A   . 5i   6      6L   �� *CapNhatKHCommand+ 5j   8   �� $ BillService��5k   7   5l   3   ��5n   .   �� 5o   2   "
 ��68    5p   6   P� 5q   :    �@J�IsMouseOver . . �5r   >   ��  �@�Cursor$
 Hand��6W   6f   5s   ;   5u   3   ._�5w   .   �� $��20(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *SDTKhachHang+ . $ PropertyChanged=�5x   /   5y   +   5z   '   �� 5{   &   $��1��61   $��1��6A   $ Vertical=�6N   $��10q�6e   .+�5|   *   �� $ 
Horizontal=�65   .+�5}   .   �� $��Tổng tiền ��68   #��[ 6J   6n   5~   +   �� 5   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5�   .   �� $��20(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *TongThanhTien+ . $ PropertyChanged=�5�   /   5�   +   5�   '   �� 5�   &   $��0��62   $��2��6B   $ Vertical=�6O   $��10q�6f   .+�5�   *   �� $ 
Horizontal=�65   .+�5�   .   �� $"��Tổng tiền trả trước��68   #��[ 6S   6w   5�   +   �� 5�   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5�   .   �� $��20(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *TongTienTraTruoc+ . $ PropertyChanged=�5�   /   5�   +   5�   '   �� 5�   &   $��1��62   $��2��6B   $ Vertical=�6O   $��10q�6f   .+�5�   *   �� $ 
Horizontal=�65   .+�5�   .   �� $��Tổng tiền còn lại��68   #��[ 6Q   6u   5�   +   �� 5�   *   $��0 0 0 1q�61   ���#5A6576$	��250��6a   $
��Left=�6p   ._�5�   .   �� $��22(�68   
�������$��0 5 0 0q�6Y   ��6j   �� *TongTienConLai+ . $ PropertyChanged=�5�   /   5�   +   5�   '   5�   #   5�      �� 5�      $��3��6%   $��0��65   $��Bottom=�6B   $��Center=�6]   $��Right=�6z   $ 10��6�   $��50��6�    5�   "     5�   &   $ MouseLeftButtonDown��65   . 5�   *      6@   �� *ThemCTHDCommand+ 5�   8   �� $ billService��5�   +   5�   '   ��5�   "   �� 5�   &   "
 ��6,   .�5�   *   ��  � �� �#04A2B36Y    5�   *   P� 5�   .    �@J�IsMouseOver . . �5�   2   ��  � �� �#038B9A6a   5�   /   5�   '   ._�5�   "   �� $ 
Horizontal=�6-   .+�5�   &   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6>   
�������$��10 0 0 0q�6]   $��Center=�6o   $��Center=�6�   $��30��6�   $��30��6�   �� 5�   &   $ 5 4 15 0q�60   
�������$��Thêm dịch vụ��6W   $��Center=�6k   $��Center=�6�   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�   '   5�   #   5�      �� 5�      $��3��6%   $��1��65   $��Center=�6B   $��Right=�6`   $ 10��6w   $��50��6�    5�   "     5�   &   $ MouseLeftButtonDown��65   . 5�   *      6@   �� *CapNhatKHCommand+ 5�   8   �� $ BillService��5�   +   5�   '   ��5�   "   �� 5�   &   "
 ��6,   .�5�   *   ��  � �� �#04A2B36Y    5�   *   P� 5�   .    �@J�IsMouseOver . . �5�   2   ��  � �� �#038B9A6a   5�   /   5�   '   ._�5�   "   �� $ 
Horizontal=�6-   .+�5�   &     $ Update=�6>   
�������$��10 0 0 0q�6_   $��Center=�6q   $��Center=�6�   $��30��6�   $��30��6�   �� 5�   &   $ 5 4 15 0q�60   
�������$��Cập nhật khách hàng��6W   $��Center=�6r   $��Center=�6�   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�   '   5�   #   5�      �� 5�      $��3��6%   $��2��65   $	��Top=�6B   $��Center=�6Z   $��Right=�6w   $ 10��6�   $��50��6�    5�   "     5�   &   $ MouseLeftButtonDown��65   . 5�   *      6@   �� *XoaCTHDCommand+ 5�   8   �� $ billService��5�   +   5�   '   ��5�   "   �� 5�   &   "
 ��6,   .�5�   *   ��  � �� �#E660606Y    5�   *   P� 5�   .    �@J�IsMouseOver . . �5�   2   ��  � �� �#E34A4A6a   5�   /   5�   '   ._�5�   "   �� $ 
Horizontal=�6-   .+�5�   &     $ Delete=�6>   
�������$��10 0 0 0q�6_   $��Center=�6q   $��Center=�6�   $��30��6�   $��30��6�   �� 5�   &   $ 5 4 15 0q�60   
�������$��Xóa dịch vụ��6V   $��Center=�6i   $��Center=�6�   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�   '   5�   #   5�      5�      5�      5�      ]� 5�      .+�5�      �� $	��Top=�6!   $��30 0 0 0q�66   $ 
Horizontal=�6H   .+�5�      �� $��25(�6%   ���#00A4FE$��Center=�6H   $��Danh sách dịch vụ��6c   6~   5�      �� 5�      $ 15��6   $	��Top=�6/   $��30 20 30 10q�6D   ���#323C48._�5�      &	   System.Windows.Controls.DataGrid	  -   5         	 Name$ gridService��5�   #   $��30 30 30 10q�68     	  GridLinesVisibility$
  None=�6M   $
��Auto=�5�   O   $��Disabled=�6   ���#323C48! 	  AutoGenerateColumns! .  " 	  
IsReadOnly" . $��95��5�   �   $	��310��6�   # �� SelectedItem# 5�   +   �� *
CTHDDaChon+��5�   A   �� *DSCTHDDichVu+$ PropertyChanged=� .  5�        5�   "   $ SelectionChanged��61   . 5�   &      6<   �� *SelectionChangedCommand+ 5�   D   �� *SelectedItems+$ gridService��5�   '   5�   #   	 5�      *
  $System.Windows.Controls.DataGridCell$ �� 
TargetType% �� Padding& J� SnapsToDevicePixels=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%G  '
        '  �    �� 5�   "   "
 
 6(   .�5�   &   ��  � ��$ 0 0 0 1q�6H   �� 5�   &    � �� �#5660706D   �� 5�   &    � ��$ 40��6?   �� 5�   &    � ��$ Center=�6J   �� 5�   &    � ��$ 	10 10 0 0q�6@   �� 5�   &    � �� 5   *   �� 5  .   "$ 
 6>   .R�5  2   �� #% z��69   #��z��6]   #��z��6�   #��z��6�   & . ._�5  6   �� #& z& 6G   ��5  :   �� 5  >   .R�5  B   �� $��texte��6e   
������ $��CharacterEllipsis=�6r   $
��auto��5  P   $
��auto��6^   $��Center=�6k   ��6�   �� *Text+5  ?   5
  7   5  3   5  /   5  '    5  &   P� 5  *    �@
 
IsSelected . . �5  .   ��  � �� �#3A45526K   5  +   5  #   �� 5  "   "
  6(   .�5  &   ��  � ��
 �����6C   �� 5  &    � �� �#5660706D   �� 5  &    � ��$ 0 0 0 2q�6H   �� 5  &    � ��$ 50��6?   �� 5  &    � ��$
 auto��6>   �� 5  &    � ��$ 20(�6A   �� 5  &    � ��$ 15 0 0 0q�6@   �� 5  &    � ��$
 Left=�6S   5  #   5     ' 	  RowStyle' 5      �� 5!  "   )  #System.Windows.Controls.DataGridRow"
  6(   .�5"  &   ��  � �� �#323C486U   �� 5#  &    � �� �#98A4B96U   �� 5$  &    � �� �#5660706D   �� 5%  &    � ��$ 0q�6H   �� 5&  &    � ��$	 5 0q�6?   �� 5'  &    � ��$ 5 10 0 0q�6@   �� 5(  &    � ��$ Center=�6J    5)  &   P� 5*  *    �@J�IsMouseOver . . �5+  .   ��  � �� �#3944516]   �� 5,  .    �@�Cursor$
 Hand��6G   6V   5-  +   P� 5.  *    �@ 
IsSelected . . �5/  .   ��  � �� �#323C486]   �� 50  .    � ��
 �����6[   51  +   53  #   ( J� InputBindings( 55     d� 56  "   ) d� Gesture$) LeftDoubleClicka�6/   * �� Command* 57  5   �� *XemChiTietCTHDCommand++ �� CommandParameter+ 58  5   �� , �� Path$, 
CTHDDaChon�- 	  Columns- 5:     0  *System.Windows.Controls.DataGridTextColumn  5;  "   ,  &System.Windows.Controls.DataGridColumn.   Width5  /System.Windows.Controls.DataGridLengthConverter$. 1* 65   /   Header$/ Tên dịch vụ��6@   1  +System.Windows.Controls.DataGridBoundColumn0   Binding0 6U   �� *DICHVU.TenDichVu+ . $ PropertyChanged=�6�     5<  "   $. 1* 65   $/ 
Đơn giá��6@   0 6R   �� *DICHVU.DonGiaDV+$ 	{0:#,##0}��1 �� ConverterCulture$1 vi-VN�� . $ PropertyChanged=�6�     5=  "   $. 1* 65   $/ Chi phí riêng��6@   0 6X   �� *ChiPhiRieng+$ 	{0:#,##0}��$1 vi-VN�� . $ PropertyChanged=�6�     5>  "   $. 1* 65   $/ Số lượng��6@   0 6S   �� *SoLuongDichVu+ . $ PropertyChanged=�6�     5?  "   $. 1* 65   $/ Thành tiền��6@   0 6U   �� *	ThanhTien+$ 	{0:#,##0}��$1 vi-VN�� . $ PropertyChanged=�6�     5@  "   $. 1* 65   $/ Trả trước��6@   0 6T   �� *TienTraTruoc+$ 	{0:#,##0}��$1 vi-VN�� . $ PropertyChanged=�6�     5A  "   $. 1* 65   $/ 
Còn lại��6@   0 6R   �� *
TienConlai+$ 	{0:#,##0}��$1 vi-VN�� . $ PropertyChanged=�6�     5B  "   $. 1* 65   $/ 
Ngày giao��6@   0 6T   �� *
NgayGiao+$ 
dd-MM-yyyy�� . $ PropertyChanged=�6�   5D     5E     ]� 5F     $
��0 10q�6    $	��Top=�6.   ��.  .+�5G     �� $��0 0 30 0q�6"   $��Right=�64   $ 10��6K   $��45��6]    5H       5I  "   $ MouseLeftButtonDown��61   . 5J  &      6<   �� *HuyBoCommand+ 5K  8   �� $ billService��5L  '   5M  #   ��5O     �� 5P  "   "
 ��6(   .�5Q  &   ��  � �� �#F8F9FA6U    5R  &   P� 5S  *    �@J�IsMouseOver . . �5T  .   ��  � �� �#E2E6EA6]   5U  +   5W  #   ._�5Y     �� $ 	15 2 15 0q�6(   ���#212529$��
Hủy bỏ��6R   $��Center=�6`   $��Center=�6{   $
��auto��5Z  +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5[     5\     �� 5]     $��0 0 10 0q�6"   $��Right=�64   $ 10��6K   $��45��6]    5^       5_  "   $ MouseLeftButtonDown��61   . 5`  &      6<   �� *XacNhanCommand+ 5a  8   �� $ billService��5b  '   5c  #   ��5e     �� 5f  "   "
 ��6(   .�5g  &   ��  � �� �#04A2B36U    5h  &   P� 5i  *    �@J�IsMouseOver . . �5j  .   ��  � �� �#038B9A6]   5k  +   5m  #   ._�5o     �� $ 	15 2 15 0q�6(   
�������$��Xác nhận��6P   $��Center=�6`   $��Center=�6{   $
��auto��5p  +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5q     5r     5s     5u     5v     5w     5x     5y     