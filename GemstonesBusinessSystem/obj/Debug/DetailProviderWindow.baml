   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   2    ,GemstonesBusinessSystem.DetailProviderWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5          �Name$  detailProvider��5
   	      DetailProviderVM � DataContext# [  5   	    � Title$ DetailProviderWindow��5   	   $	��470��6&   $	��750��63    � 
ResizeMode$ CanMinimize=�6?    � WindowStartupLocation$ CenterScreen=�6X   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+	   CommandParameter	 5   $   �� 
 �� ElementName$
 detailProvider��5       � 	Resources 5       �� 
TargetType �� Property �� Value��%}   '��       �� 5   
   " ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 15(�6)   66   5      5      .��5      ]� ���#2A323C.+�5   
   � $	��Top=�6   $��Center=�6$   $��55��6?   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5      ��  �� Padding$ 20 0 0 0q�6   $��Center=�6+   $ ��Thông tin nhà cung cấp��6F   
�������$��25(�6w   $��DemiBold"�6�   6�   5      �� 5   
   $
��Auto=�6   .��5      ]� ���#2A323C.+�5      ��  �� CornerRadius$ 15��6   ���#323C48$	��Top=�6A   $��
30 30 30 0q�6V   ._�5       � $��20q�6    � ColumnDefinitions 5!      �� 5"      $��1* �6/   6<   �� 5#      $��1* �6/   6<   .+�5%      �� $��0��6%    �� Orientation$ Vertical=�65   $��15 18 10 10q�6L   .+�5&      �� $��0 0 0 0q�6%   ���#5A6576$��
10 0 10 10q�6U   ._�5'   "   �� $ 
Horizontal=�6-   .+�5(   &   �� $	��130��61   $ 
Horizontal=�6=   $��Center=�6V   .+�5)   *   �� $��Mã nhà cung cấp ��64   6O   �� 5*   *   $��*��64   
���  ��6P   5+   '   �� 5,   &    �� 
IsReadOnly . $��14(�6A   
�������$��0 0 0 0q�6�   $	��150��6�   $
��Left=�6�   ��6c   �� *CTNhaCungCap.MaNhaCungCap+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�.��6  5-   '   5.   #   5/      �� 50      $��0 0 0 0q�6%   ���#5A6576$��
10 0 10 10q�6U   ._�51   "   �� $ 
Horizontal=�6-   .+�52   &   �� $	��130��61   $ 
Horizontal=�6=   $��Center=�6V   .+�53   *   �� $��Tên nhà cung cấp ��64   6P   �� 54   *   $��*��64   
���  ��6P   55   '   �� 56   &   $��14(�6/   
�������$��0 0 0 0q�6�   $	��150��6�   $
��Left=�6�   ��6Q   �� *TenNhaCungCap+ . $ PropertyChanged=�.��6�   57   '   58   #   59      �� 5:      $��0 0 0 0q�6%   ���#5A6576$��
10 0 10 10q�6U   ._�5;   "   �� $ 
Horizontal=�6-   .+�5<   &   �� $	��130��61   $ 
Horizontal=�6=   $��Center=�6V   .+�5=   *   �� $��Địa chỉ ��64   6G   �� 5>   *   $��*��64   
���  ��6P   5?   '   �� 5@   &   $��14(�6.   
�������$��0 0 0 0q�6�   $	��150��6�   $
��Left=�6�   ��6P   �� *	DiaChiNCC+ . $ PropertyChanged=�.��6�   5A   '   5B   #   5C      �� 5D      $��0 0 0 0q�6%   ���#5A6576$��
10 0 10 10q�6U   ._�5E   "   �� $ 
Horizontal=�6-   .+�5F   &   �� $	��130��61   $ 
Horizontal=�6=   $��Center=�6V   .+�5G   *   �� $��Số điện thoại ��64   6M   �� 5H   *   $��*��64   
���  ��6P   5I   '   �� 5J   &   $��14(�6.   
�������$��0 0 0 0q�6�   $	��150��6�   $
��Left=�6�   ��6P   �� *SoDienThoaiNCC+ . $ PropertyChanged=�.��6�   5K   '   5L   #   5M      5N      �� 5O      $��1��6%   $ Vertical=�65   $��15 18 10 10q�6L   .+�5P      �� $��0 0 0 0q�6%   ���#5A6576$��	0 0 10 10q�6U   ._�5Q   "   �� $ Vertical=�6-   .+�5R   &   �� $
��Left=�61   $	��120��6L   $ 
Horizontal=�6X   $��Center=�6q   .+�5S   *   �� $��Email ��64   6E   �� 5T   *   $��*��64   
���  ��6P   5U   '   �� 5V   &   $��14(�6/   
�������$��0 0 0 0q�6�   $	��200��6�   $
��Left=�6�   ��6Q   �� *
EmailNCC+ . $ PropertyChanged=�.��6�   5W   '   5X   #   5Y      5Z      5[      5\      �� 5]      $��30 10q�6   $	��Top=�6+   $��Center=�6A   ���#00A4FE$��25(�6q   $7��1Danh sách hóa đơn hàng hóa đã giao dịch��6   5^      �� 5_      $ 15��6   ���#323C48$
��30 0q�6@   $	��Top=�6N   ._�5`      &   System.Windows.Controls.DataGrid  -   5          Name$ GridProductPhieuBanHang��5`   ,   $��15q�6      GridLinesVisibility$
 None=�6M   $��Visible=�5a   +   $��Disabled=�6^      SelectionUnit$ FullRow=�6�   ���#323C48   RowDetailsVisibilityMode$ VisibleWhenSelected=�5b   A   $	��350��6p      SelectionMode$ Extended=�5d   +      AutoGenerateColumns .     
IsReadOnly .  �� SelectedItem 5c   +   �� *	PMHDaChon+$ PropertyChanged=� .  �� Mode$ TwoWay=���5d   D   �� *DSPMHNhaCungCap+ 5e      *  $System.Windows.Controls.DataGridCell  �� 
TargetType! �� Padding" J� SnapsToDevicePixels# �� Triggers$ P� Property% P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%q  '        '      �� 5f      "  6$   .�5g   "   ��  � ��$ 0 0 0 1q�6D   �� 5h   "    � �� �#5660706@   �� 5i   "    � ��$ 40��6;   �� 5j   "    � ��$ Center=�6F   �� 5k   "    � ��$ 	10 10 0 0q�6<   �� 5l   "    � �� �#98A4B96Q   �� 5m   "    � �� 5n   &   �� 5o   *   "   6:   .R�5p   .   �� #! z��65   #��z��6Y   #��z��6�   #��z��6�   " . ._�5q   2   �� #" z" 6C   ��5r   6   �� 5s   :   .R�5t   >   �� $��texte��6a   
������ $��CharacterEllipsis=�6n   $
��auto��5u   P   $
��auto��6^   $��Center=�6k   ��6�   �� *Text+5v   ;   5x   3   5y   /   5z   +   5|   #   # 5}   "   P� 5~   &   $ �@ 
IsSelected% . . �5   *   ��  � �� �#3A45526G   5�   '   5�      �� 5�      "  6$   .�5�   "   ��  � ��
 �����6?   �� 5�   "    � �� �#5660706@   �� 5�   "    � ��$ 0 0 0 2q�6D   �� 5�   "    � ��$ 50��6;   �� 5�   "    � ��$
 auto��6:   �� 5�   "    � ��$ 15(�6=   �� 5�   "    � ��$ 15 0 0 0q�6<   �� 5�   "    � ��$
 Left=�6O   5�      5�      &   RowStyle& 5�      �� 5�      )	  #System.Windows.Controls.DataGridRow" 	 6$   .�5�   "   ��  � �� �#323C486Q   �� 5�   "    � �� �#98A4B96Q   �� 5�   "    � �� �#5660706@   �� 5�   "    � ��$ 0q�6D   �� 5�   "    � ��$	 5 0q�6;   �� 5�   "    � ��$ 5 10 0 0q�6<   �� 5�   "    � ��$ Center=�6F   # 5�   "   P� 5�   &   $ �@J�IsMouseOver% . . �5�   *   ��  � �� �#3944516Y   �� 5�   *    �@�Cursor$
 Hand��6C   6R   5�   '   P� 5�   &   $ �@	 
IsSelected% . . �5�   *   ��  � �� �#3A45526Y   5�   '   5�      ' J� InputBindings' 5�      d� 5�      ( d� Gesture$( LeftDoubleClicka�6+   ) �� Command) 5�   7   �� *ChiTietPMHCommand+* �� CommandParameter* 5�   7   �� + �� Path$+ 	PMHDaChon�,   Columns, 5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�      ,  &System.Windows.Controls.DataGridColumn-   Width5  /System.Windows.Controls.DataGridLengthConverter$- 1* 61   .   Header$. Số phiếu��6<   1  +System.Windows.Controls.DataGridBoundColumn/   Binding/ 6N   �� *MaPhieuMuaHang+
  5�      $- 1* 61   $. Ngày tạo��6<   / 6O   �� *NgayLapPhieuMua+0 �� StringFormat$0 
dd-MM-yyyy��
  5�      $- 1* 61   $. Tổng số lượng��6<   / 6T   �� *TongSoLuongMua+
  5�      $- 1* 61   $. Tổng tiền��6<   / 6P   �� *TongThanhTien+$0 	{0:#,##0}��1 �� ConverterCulture$1 vi-VN��6�   5�      5�      ]� 5�      ��.  $��
 0 10 0 20q�62   .+�5�      �� $��0 0 30 0q�6   $��Right=�60   $ 10��6G   $��45��6Y    5�        5�      $ MouseLeftButtonDown��6-   . 5�   "      68   �� *HuyBoCommand+	 5�   8   �� $
 detailProvider��5�   #   5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#F8F9FA6Q   # 5�   "   P� 5�   &   $ �@J�IsMouseOver% . . �5�   *   ��  � ��
 �����6W   5�   '   5�      ._�5�      �� $ 	15 2 15 0q�6$   ���#212529$��
Hủy bỏ��6N   $��Center=�6\   $��Center=�6w   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      �� 5�      $��0 0 10 0q�6   $��Right=�60   $ 10��6G   $��45��6Y    5�        5�      $ MouseLeftButtonDown��6-   . 5�   "      68   �� *XacNhanCommand+	 5�   8   �� $
 detailProvider��5�   #   5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#04A2B36Q   # 5�   "   P� 5�   &   $ �@J�IsMouseOver% . . �5�   *   ��  � �� �#038B9A6Y   5�   '   5�      ._�5�      �� $ 	15 2 15 0q�6$   
�������$��Cập nhật��6L   $��Center=�6\   $��Center=�6w   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      5�      5�      