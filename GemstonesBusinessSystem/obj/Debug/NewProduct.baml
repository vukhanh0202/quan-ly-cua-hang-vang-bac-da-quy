   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   (    "GemstonesBusinessSystem.NewProduct   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5          �Name$  NewProductWindow��5
   	      NewProductVM � DataContext# [  5   	    � Title$ Thêm sản phẩm mới��5   	   $	��670��6#   $
��1080��60    � WindowStartupLocation$ CenterScreen=�6=   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   �� 	 �� ElementName$	 NewProductWindow��5      
 � 	Resources
 5       �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   " ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5      ]� ��. ���#2A323C.+�5    
   � $��60��6   $��Center=�6   $	��Top=�66   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6`   .+�5!      �� $��Center=�6    �� Padding$ 20 0 0 0q�63   $��Thêm mới sản phẩm��6F   
�������$��25(�6r   $��DemiBold"�6�   6�   5"      �� 5#   
   .��5%      � $	��Top=�6   ���#2A323C$��10 10 10 10q�6=    � ColumnDefinitions 5&      �� 5'      $��6* �6'   64   �� 5(      $	��10* �6'   65   .+�5*      ��  �� Orientation$ Vertical=�6   $��0��64   .+�5+      �� $��30 10 20 10q�6    �� CornerRadius$ 15��62   ���#323C48._�5,      ]� $��20q�6%   .+�5-      ]� $	��Top=�6(   $��
10 10 10 0q�6=   .+�5.   "   �� $��Ảnh sản phẩm��6-   
�������$��24(�6T   $��10 5 0 0q�6b   $��Medium"�6t   6�   �� 5/   "   $��Right=�6.   $��Thêm ảnh��6J   $��Center=�6Z   $��0 5 0 0q�6u   $
��auto��50   +   $��22(�69   $:��4/GemstonesBusinessSystem;component/fonts/#Baloo Da 2)�6G    52        53   "   $ MouseLeftButtonDown��61   . 54   &      6<   �� *ThayDoiAnhCommand+55   '   56   #   ��58   &   �� 59   *   " ��60   .�5:   .   ��  � �� �#0088FF6]    �� Triggers 5;   .   P� 5<   2    P� Property �@J�IsMouseOver P� Value . . �5=   6   ��  � �� �#084E8A6e   �� 5>   6    �@�Cursor$
 Hand��6O   6^   �� 5?   6    � ��$ 	Underline{�6X   6l   5@   3   5B   +   5D   #   5E      �� 5G      $��20 20 20 30q�6$   $	��250��6X   $	��250��6e   ��69   �� *imageSource+6s   5H      5I      5J      ]� 5L      $��1��6   ��.  .+�5M      �� ���#323C48$ 15��62   $��
20 10 30 0q�6D   $	��Top=�6X   ._�5N      ]� $��20q�6$   .+�5O      �� $	��Top=�6)   $��Sản phẩm��6>   
�������$��24(�6a   $��	20 15 0 0q�6o   $��Medium"�6�   6�   �� 5P      $	��Top=�6)   $ Vertical=�6>   $��20 20 20 10q�6U   .+�5Q   "   �� $ 
Horizontal=�6-   .+�5R   &   �� $��Tên sản phẩm ��60   #��[ 6E   6i   �� 5S   &   $��*��60   
���  ��#��[ 6J   6n   5T   #   �� 5U   "   ���#505B6A$��20(�6B   
�������$��0 5 0 0q�6d   ��6u   �� *SanPhamMoi.TenSanPham+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=� �� NotifyOnTargetUpdated . .��6�   5V   #   5W      5Y      5Z      �� 5[      $	��Top=�6   $ 15��62   ���#323C48$��20 30 30 20q�6Y   ._�5\      ]� $��20q�6%   .+�5]      ]� $	��Top=�6(   $��
10 10 10 0q�6=   .+�5^   "   �� $��Phân loại��6-   
�������$��24(�6Q   $��10 5 0 0q�6_   $��Medium"�6q   6�   �� 5_   "   $��Right=�6.   $��Thêm loại��6J   $��Center=�6[   $��0 5 10 0q�6v   $
��auto��5`   +   $��24(�69   $:��4/GemstonesBusinessSystem;component/fonts/#Baloo Da 2)�6G    5b        5c   "   $ MouseLeftButtonDown��61   . 5d   &      6<   �� *ThemLSPCommand+5e   '   5f   #   ��5h   &   �� 5i   *   " ��60   .�5j   .   ��  � �� �#0088FF6]    5k   .   P� 5l   2    �@J�IsMouseOver . . �5m   6   ��  � �� �#084E8A6e   �� 5n   6    �@�Cursor$
 Hand��6O   6^   �� 5o   6    � ��$ 	Underline{�6X   6l   5p   3   5r   +   5t   #   5u      �� 5v      $ 
Horizontal=�6)   $	��Top=�6C   .+�5w   "   �� $ Vertical=�6.   $��
20 0 20 10q�6E   .+�5x   &   �� $ 
Horizontal=�61   .+�5y   *   �� $��Loại sản phẩm ��64   #��[ 6J   6n   �� 5z   *   $��*��64   
���  ��#��[ 6N   6r   5{   '   �� 5|   &   -   5         ��Name$ comboBoxType��5|   E   ���#505B6A$��15(�6[    �� DisplayMemberPath$ TenLoaiSanPham��5}   �   $��0 5 0 5q�6�   $	��150��6�   $
��Left=�6�   
������� �� SelectedItem 5|   i   �� *	LSPDaChon+$ PropertyChanged=� . ��5}   $   ��  �� Path$ DSLoaiSanPham�$ PropertyChanged=� .  5~   *     5   .   $ SelectionChanged��6=   . 5�   2      6H   �� *SelectionChangeCommand+5�   /   5�   '   5�   #   �� 5�   "   $	��Top=�6-   $ Vertical=�6B   $��
20 0 20 10q�6Y   .+�5�   &   �� $��Mã loại sản phẩm��60   #��[ 6H   6l   �� 5�   &   ���#505B6A)  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Chọn loại sản phẩm��6E   $��20(�6y   
�������$��0 5 0 0q�6   $
��auto��61  $
��Left=�6>   �� 
IsReadOnly . ��6�   �� *LSPDaChon.MaLoaiSanPham+ . $ PropertyChanged=� . .��6k  5�   '   5�   #   5�      �� 5�      $
��Left=�6)   $ Vertical=�6?   $��
20 0 20 20q�6V   .+�5�   "   �� $��
Đơn vị��6,   #��[ 6:   6^   �� 5�   "   ���#505B6A$  Chọn loại sản phẩm��6A   $��20(�6u   
�������$��0 5 0 0q�6�   $
��auto��6*  $
��Left=�67   . ��6�   �� *LSPDaChon.DVT.TenDVT+ . $ PropertyChanged=� . .��6d  5�   #   5�      �� 5�      $
��Left=�6)   $ Vertical=�6?   $��
20 0 20 10q�6V   .+�5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $%��Phần trăm lợi nhuận (%) ��60   #��[ 6P   6t   �� 5�   &   $��*��60   
���  ��#��[ 6J   6n   5�   #   �� 5�   "   ���#505B6A$��0 5 0 0q�6@   $* $Vui lòng chọn loại sản phẩm��6Q   $��20(�6�   
�������$
��auto��6M  $
��Left=�6Z   . ��6�   Z� ��6�   �� *LSPDaChon.PhanTramLoiNhuan+ . $ PropertyChanged=� . .��6�  5�   #   5�      5�      5�      �� 5�      $��0 0 0 20q�6!   $��Right=�63   $��Bottom=�6O   $ 
Horizontal=�6g   .+�5�      �� $��0 0 10 0q�6"   $��Right=�64   $ 10��6K   $��45��6]    5�        5�   "   $ MouseLeftButtonDown��61   . 5�   &      6<   �� *XacNhanCommand+ 5�   8   �� $	 NewProductWindow��5�   '   5�   #   ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � �� �#04A2B36U    5�   &   P� 5�   *    �@J�IsMouseOver . . �5�   .   ��  � �� �#038B9A6]   5�   +   5�   #   ._�5�      �� $ 	15 2 15 0q�6(   
�������$��Xác nhận��6P   $��Center=�6`   $��Center=�6{   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      �� 5�      $��0 0 20 0q�6"   $��Right=�64   $ 10��6K   $��45��6^    5�        5�   "   $ MouseLeftButtonDown��61   . 5�   &      6<   �� *HuyBoCommand+ 5�   8   �� $	 NewProductWindow��5�   '   5�   #   ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � �� �#F8F9FA6U    5�   &   P� 5�   *    �@J�IsMouseOver . . �5�   .   ��  � �� �#038B9A6]   5�   +   5�   #   ._�5�      �� $ 	15 2 15 0q�6(   ���#212529$��
Hủy bỏ��6R   $��Center=�6`   $��Center=�6{   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      5�      5�      5�      