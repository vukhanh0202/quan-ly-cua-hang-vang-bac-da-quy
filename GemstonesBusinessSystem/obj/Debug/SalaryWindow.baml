   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   *    $GemstonesBusinessSystem.SalaryWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:GemstonesBusinessSystem     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $��SalaryWD��5
   	     � Title$  Tính lương��5   
   $	��650��6   $	��900��6*    � WindowStartupLocation$ CenterScreen=�66      SalaryVM � DataContext# [  5   	   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ SalaryWD��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%M  &	       '�� s     �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 25(�6)   66   5      �� 5   
   "
 ��6   .�5      ��  � �� 6+   Z� 6>   �� 5       � �� �#5660706>   �� 5       � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5       ]� ��. ���#2A323C.+�5!   
   � $��45��6   $	��Top=�6   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6E   .+�5"      ��  �� Padding$ 20 5 0 0q�6   $��Tính lương��6+   
�������$��22(�6P   $��DemiBold"�6^   6v   5#      � 5$   
   $	��Top=�6   ���#2A323C$��
10 10 10 0q�69   .+�5%      ]� $	��Top=�6   $��10q�6-   .+�5'      �� ���#323C48 �� CornerRadius$ 15��6.   $	��Top=�6@   $��
30 30 30 0q�6U   ._�5(      ��  �� Orientation$ Vertical=�6"   $��30q�69   .+�5)      �� $ 
Horizontal=�6%   .+�5*      �� $��Theo tháng ��6(   #��[ 6;   6_   �� 5+      $��*��6(   
���  ��#��[ 6B   6f   �� 5,      $��
Theo năm ��6(   #��[ 69   $��	245 0 0 0q�6[   6p   �� 5-      $��*��6(   
���  ��#��[ 6B   6g   5.      �� 5/      $ 
Horizontal=�6%   .+�50      �� $��0 5 0 0 q�6'   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Chọn tháng��69   $��22(�6e   
�������$	��250��53      $
��Left=�54      ��51   #   ��  �� Path$ DSThang� �� SelectedValue 52   #   �� *ThangDaChon+ �� UpdateSourceTrigger$ PropertyChanged=� �� NotifyOnSourceUpdated .  55   "     56   &   $ SelectionChanged��65   . 57   *      6@   �� *Filter+58   '   5:      �� 5;      
�������$ Chọn năm��6:   $��22(�6d   $��
140 5 0 0 q�6r   $	��250��5=      $
��Left=�5>       5<   #   �� *	NamDaChon+$ PropertyChanged=� . ��5>   :   �� $ DSNam� 5?   "     5@   &   $ SelectionChanged��65   . 5A   *      6@   �� *Filter+5B   '   5D      5E      5F      5G      �� 5H      $��0 1 0 0q�6    J� Opacity$	 0.7Q�63   ���#566070$	��Top=�6W   $��20 10q�6l   5I      � 5J       � RowDefinitions 5K      �� 5L      $��9* �6(   66   �� 5M      $��2* �6(   66   .+�5P      �� $��0��6   ���#323C48$ 15��6?   $��
30 10 30 0q�6Q   ._�5Q      &   System.Windows.Controls.DataGrid  $��15q�6#      GridLinesVisibility$
 None=�6/   $��Visible=�6�   $��Disabled=�6�   ���#323C48   AutoGenerateColumns .     
IsReadOnly .  �� SelectedItem 6J   �� $
 temp�$ PropertyChanged=���5R   @   �� *	DSLuongNV+ 5S        5T   "   $ SelectionChanged��61   . 5U   &      6<   �� *SelectionChangedCommand+ 5V   D   �� *SelectedItems+$ GridProduct��5W   '   5X   #   	 5Z      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels �� Triggers  P� Property! P� Value=	  7System.Windows.Controls.Primitives.DataGridColumnHeader��%q  '        '	      �� 5[   "   "
  6(   .�5\   &   ��  � ��$ 0 0 0 1q�6H   �� 5]   &    � �� �#5660706D   �� 5^   &    � ��$ 40��6?   �� 5_   &    � ��$ Center=�6J   �� 5`   &    � ��$ 	10 10 0 0q�6@   �� 5a   &    � �� �#98A4B96U   �� 5b   &    � �� 5c   *   �� 5d   .   "  6>   .R�5e   2   �� # z��69   #��z��6]   #��z��6�   #��z��6�    . ._�5f   6   �� # z 6G   ��5g   :   �� 5h   >   .R�5i   B   �� $��texte��6e   
������ $��CharacterEllipsis=�6r   $
��auto��5j   P   $
��auto��6^   $��Center=�6k   ��6�   �� *Text+5k   ?   5m   7   5n   3   5o   /   5q   '    5r   &   P� 5s   *     �@ 
IsSelected! . . �5t   .   ��  � �� �#3A45526K   5u   +   5w   #   �� 5x   "   "
 	 6(   .�5y   &   ��  � ��
 �����6C   �� 5z   &    � �� �#5660706D   �� 5{   &    � ��$ 0 0 0 2q�6H   �� 5|   &    � ��$ 50��6?   �� 5}   &    � ��$
 auto��6>   �� 5~   &    � ��$ 15(�6A   �� 5   &    � ��$ 15 0 0 0q�6@   �� 5�   &    � ��$
 Left=�6S   5�   #   5�      "   RowStyle" 5�      �� 5�   "   )
  #System.Windows.Controls.DataGridRow"
 
 6(   .�5�   &   ��  � �� �#323C486U   �� 5�   &    � �� �#98A4B96U   �� 5�   &    � �� �#5660706D   �� 5�   &    � ��$ 0q�6H   �� 5�   &    � ��$	 5 0q�6?   �� 5�   &    � ��$ 5 10 0 0q�6@   �� 5�   &    � ��$ Center=�6J    5�   &   P� 5�   *     �@J�IsMouseOver! . . �5�   .   ��  � �� �#3944516]   �� 5�   .    �@�Cursor$
 Hand��6G   6V   5�   +   P� 5�   *     �@
 
IsSelected! . . �5�   .   ��  � �� �#3A45526]   5�   +   5�   #   #   Columns# 5�      0  *System.Windows.Controls.DataGridTextColumn  5�   "   ,  &System.Windows.Controls.DataGridColumn$   Width5  /System.Windows.Controls.DataGridLengthConverter$$ 1* 65   %   Header$% Mã NV��6@   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6O   �� *
MaNhanVien+  5�   "   $$ 2* 65   $% Tên nhân viên��6@   & 6W   �� *TenNhanVien+  5�   "   $$ 2* 65   $% Lương cơ bản��6@   & 6V   �� *
LuongCoBan+' �� StringFormat$' 	{0:#,##0}��( �� ConverterCulture$( vi-VN��  5�   "   $$ 2* 65   $% Số sản phẩm bán��6@   & 6Y   �� *	TongSLBan+  5�   "   $$ 2* 65   $% Tổng tiền thưởng��6@   & 6[   �� *PhanTramHoaHong+  5�   "   $$ 2* 65   $% Tổng lương��6@   & 6U   �� *	TongLuong+$' 	{0:#,##0}��$( vi-VN��5�      5�      ]� 5�      $��1��6    ��.  ���#2A323C.+�5�      �� $��
20 10 30 0q�6!   $��Right=�65   $ 10��6L   $��45��6^    5�        5�   "   $ MouseLeftButtonDown��61   . 5�   &      6<   �� *HuyBoCommand+ 6]   �� $ SalaryWD��5�   '   5�   #   ��5�      �� 5�   "   "
 ��6(   .�5�   &   ��  � �� �#F8F9FA6U    5�   &   P� 5�   *     �@J�IsMouseOver! . . �5�   .   ��  � �� �#038B9A6]   5�   +   5�   #   ._�5�      �� $ 	15 2 15 0q�6(   
���   �$��
Hủy bỏ��6P   $��Center=�6^   $��Center=�6y   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      �� 5�      $��0 10 0 0q�6!   $��Right=�63   $ 10��6J   $��45��6\    5�        5�   "   $ MouseLeftButtonDown��61   . 5�   &      6<   �� *XuatExcelCommand+5�   '   5�   #   ��5�      �� 5�   "   "
 ��6(   .�5�   &   ��  � �� �#04A2B36U    5�   &   P� 5�   *     �@J�IsMouseOver! . . �5�   .   ��  � �� �#038B9A6]   5�   +   5�   #   ._�5�      �� $ 	15 2 15 0q�6(   
�������$��Xuất Excel��6P   $��Center=�6b   $��Center=�6}   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      5�      5�      5�      