   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   8    2GemstonesBusinessSystem.NewDetailBillServiceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5        $��newDetailBillServiceVM��5	   	      NewDetailBillServiceVM  � DataContext#  [  5   	    � Title$$ Thêm dịch vụ vào phiếu��5   	   $	��700��6(   $
��1000��65    � WindowStartupLocation$ CenterScreen=�6B   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ newDetailBillServiceVM��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 24(�6)   66   5      5      .��5      ]� ���#2A323C.+�5   
   � $	��Top=�6   $��Center=�6$   $��60��6?   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5      ��  �� Padding$ 20 5 0 0q�6   $��Center=�6+   $$��Thêm dịch vụ vào phiếu��6F   
�������$��25(�6w   $��DemiBold"�6�   6�   5      �� 5   
   $	��Top=�6   $��0 1 0 0q�6&   ���#5660706X   ]� 5   
   .+�5       ��  �� CornerRadius$ 15��6   ���#323C48$	��Top=�6=   $��30 40 30 30q�6R   ._�5!      � $��40q�6    � ColumnDefinitions 5"      �� 5#      6-   �� 5$      6-    � RowDefinitions 5&      �� 5'      6*   �� 5(      6*   �� 5)      6*   �� 5*      6*   .+�5,      �� $��0��6!   $��0��61    �� Orientation$ Vertical=�6>   $��10q�6U   .+�5-      �� $ 
Horizontal=�6%   .+�5.      �� $��Tên dịch vụ ��6(   #��[ 6<   6`   �� 5/      $��*��6(   
���  ��#��[ 6B   6f   50      �� 53      -   5         ��Name$ comboBoxType��53   g   ���#505B6A)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Chọn dịch vụ��69   $��20(�6}    �� DisplayMemberPath$ 	TenDichVu��54   I   $��0 5 0 5q�6g   $	��305��6x   $
��Left=�6�   
������� �� SelectedItem 53   �   �� *
DVDaChon+��54   #   ��  �� Path$ DSDichVu� 55        56   "   $ SelectionChanged��61   . 57   &      6<   �� *SelectionChangeCommand+58   #   5:      5;      �� 5<      $��0��6!   $��1��61   $ Vertical=�6>   $��10q�6U   .+�5=      �� $ 
Horizontal=�6%   .+�5>      �� $��Số lượng ��6(   #��[ 69   6]   �� 5?      $��*��6(   
���  ��#��[ 6B   6f   5@      �� 5A      ���#505B6A$ Nhập số lượng��68   $��22(�6g   
�������$��0 5 0 0q�6�   ��6�   �� *CTHDMoi.SoLuongDichVu+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�.��6   5B        5C   "   $ TextChanged��61   . 5D   &      6<   �� *SLThayDoiCommand+5E   #   .��5F   6   5G      5H      �� 5I      $��0��6!   $��2��61   $ Vertical=�6>   $��10q�6U   .+�5J      �� $ 
Horizontal=�6%   .+�5K      �� $��Chi phí riêng ��6(   #��[ 6>   6b   �� 5L      $��*��6(   
���  ��#��[ 6B   6f   5M      �� 5N      ���#505B6A$  Nhập chi phí phát sinh��69   $��22(�6q   
�������$��0 5 0 0q�6�   ��6�   �� *CTHDMoi.ChiPhiRieng+ �� StringFormat$ 	{0:#,##0}�� �� ConverterCulture$ vi-VN�� . $ PropertyChanged=�.��6=   5O        5P   "   $ TextChanged��61   . 5Q   &      6<   �� *ChiPhiThemThayDoiCommand+5R   #   .��5S   6   5T      5U      �� 5V      $��1��6!   $��0��61   $ Vertical=�6>   $��10q�6U   .+�5W      �� $ 
Horizontal=�6%   .+�5X      �� $��Ngày giao ��6(   #��[ 6:   6^   �� 5Y      $��*��6(   
���  ��#��[ 6B   6f   5Z      (  "System.Windows.Controls.DatePicker  5[      $	��150��6'   
�������
�������$��18(�5\   9   $
��Left=�6G    ( $MaterialDesignFloatingHintDatePicker#��[ 6b      SelectedDate 5[   G   �� *CTHDMoi.NgayGiao+$ 
dd-MM-yyyy�� . $ PropertyChanged=�5]      5^      �� 5_      $��0��6"   $��3��62   $ Vertical=�6?   $��10q�6V   .+�5`      �� $ 
Horizontal=�6%   .+�5a      �� $"��Tổng tiền trả trước��6(   #��[ 6C   6g   �� 5b      #��[ 6�   ��6(   �� *TienYeuCauTraTruoc+$ PropertyChanged=� . 6�   �� 5c      $��*��6(   
���  ��#��[ 6B   6f   5d      �� 5e      ���#505B6A$" Nhập tiền trả trước��68   $��22(�6m   
�������$��0 5 0 0q�6�   ��6�   �� *CTHDMoi.TienTraTruoc+$ 	{0:#,##0}��$ vi-VN�� . $ PropertyChanged=�.��6:   5f        5g   "   $ TextChanged��61   . 5h   &      6<   �� *TienTraTruocThayDoiCommand+5i   #   .��5j   6   5k      5l      �� 5m      $��1��6"   $��3��62   $ Vertical=�6?   $��10q�6V   .+�5n      �� $ 
Horizontal=�6%   .+�5o      �� $��Tổng tiền còn lại��6(   #��[ 6A   6e   5p      �� 5q      ���#505B6A �� 
IsReadOnly . $��22(�6J   
�������$��0 5 0 0q�6k   ��6|   �� *
TienConLai+ . $ PropertyChanged=�.��6�   5r      5s      �� 5u      $��1��6"   $��2��62   $ Vertical=�6?   $��10q�6V   .+�5v      �� $ 
Horizontal=�6%   .+�5w      �� $��Thành tiền��6(   #��[ 6:   6^   5x      �� 5y      ���#505B6A . $��22(�6J   
�������$��0 5 0 0q�6k   ��6|   �� *	ThanhTien+ . $ PropertyChanged=�.��6�   5z      5{      5|      5}      �� 5~      $
��30 0q�6   $	��Top=�6#   $��0 1 0 0q�68   ���#5660706j   ]� 5      $	��Top=�6   ��.  .+�5�      �� $��0 0 30 0q�6   $��Right=�6,   $ 10��6C   $��45��6U    5�        5�      $ MouseLeftButtonDown��6)   . 5�         64   �� *HuyBoCommand+ 5�   8   �� $ newDetailBillServiceVM��5�      5�      ��5�      �� 5�      "
 ��6    .�5�      ��  � �� �#F8F9FA6M    �� Triggers 5�      P� 5�   "    P� Property �@J�IsMouseOver  P� Value  . . �5�   &   ��  � �� �#E2E6EA6U   5�   #   5�      ._�5�      �� $ 	15 2 15 0q�6    ���#212529$��
Hủy bỏ��6J   $��Center=�6X   $��Center=�6s   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      �� 5�      $��0 0 10 0q�6   $��Right=�6,   $ 10��6C   $��45��6U    5�        5�      $ MouseLeftButtonDown��6)   . 5�         64   �� *XacNhanCommand+ 5�   8   �� $ newDetailBillServiceVM��5�      5�      ��5�      �� 5�      "
 ��6    .�5�      ��  � �� �#04A2B36M    5�      P� 5�   "    �@J�IsMouseOver  . . �5�   &   ��  � �� �#038B9A6U   5�   #   5�      ._�5�      �� $ 	15 2 15 0q�6    
�������$��Xác nhận��6H   $��Center=�6X   $��Center=�6s   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      5�      