   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   /    )GemstonesBusinessSystem.InfoAccountWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:GemstonesBusinessSystem     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $��InfoStaffWD��5
   	      InfoStaffVM  � DataContext#  [  5   	    � Title$ Thông tin nhân viên��5   	   $	��530��6%   $	��880��62    � WindowStartupLocation$ CenterScreen=�6>   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ InfoStaffWD��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5      ]� ��. ���#2A323C.+�5   
   � $��45��6   $	��Top=�6   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6E   .+�5      ��  �� Padding$ 20 5 0 0q�6   $��Thông tin tài khoản��6+   
�������$��22(�6Y   $��DemiBold"�6g   6   5      � 5   
   $	��Top=�6   ���#2A323C$��10 10 10 10q�69    � ColumnDefinitions 5      �� 5      $��6* �6#   60   �� 5       $	��11* �6#   61   .+�5"      ��  �� Orientation$ Vertical=�6   $��0��60   .+�5#      ��  �� CornerRadius$ 15��6   ���#323C48$��
20 20 0 20q�6@   ._�5$      ]� $��27q�6    .+�5%      ]� $	��Top=�6$   .+�5&      �� $��Ảnh��6)   
�������$��18(�6G   $��10 5 0 0q�6U   $��Medium"�6g   6}   5'      � 5(      $��
0 20 10 10q�6   $	��230��63   $	��230��6@    5)        5*   "   $ MouseLeftButtonDown��61   . 5+   &      6<   �� *ThayDoiAnhCommand+5,   '   5-   #   .+�5/      �� $	��200��6C   $	��200��6P   ��6$   �� *imageSource+6^   �� 50      $ 
Horizontal=�6)   $��Center=�6B   $	��200��6_   $	��200��6k   .+�51   "   '  !MaterialDesignThemes.Wpf.PackIcon  $��45 0 5 0q�6:   $��30��6L   $��30��6W      Kind$ Pencil=�6c   $��Center=�6q   $��Center=�6�   
���   �6�   �� 52   "   $��Center=�6,   $��Thay đổi��6I   $��23(�6Y   $��Center=�6g   
���   �6�   ��53   "   �� 54   &   "
 ��6,   .�55   *   ��  � �� 6G   Z� 6Z   �� 56   *    �@J�Opacity$ 0Q�6D   6P    �� Triggers 57   *   P� 58   .    P� Property �@J�IsMouseOver P� Value . . �59   2   ��  � y�$ Visible=�6O   6a   �� 5:   2    � ��
 �����6_   �� 5;   2    �@J�Opacity$	 0.3Q�6L   6Z   �� 5<   2    �@�Cursor$
 Hand��6K   6Z   5=   /   5?   '   5A      5B      5C      5D      5E      ]� 5F      $��1��6   ��.  .+�5G      �� $ 15��6   $	��Top=�6+   ���#323C48$��20q�6U   ._�5H      ]� $��20q�6    .+�5I      �� $	��Top=�6$   $��
Thông tin��69   
�������$��18(�6]   $��	10 10 0 0q�6k   $��Medium"�6~   6�   �� 5J      $	��Top=�6%   $ Vertical=�6:   $��10q�6Q   .+�5K      �� $ 
Horizontal=�6)   .+�5L   "   �� $��Tên nhân viên ��6,   #��[ 6B   6f   �� 5M   "   $��*��6,   
���  ��#��[ 6F   6j   5N      �� 5O      ���#505B6A$��0 5 0 0 q�6<   $��20(�6N   
���������6o   �� *CTNhanVien.TenNhanVien+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�.��6�   5P      5Q      �� 5R      $��Bottom=�6%   $ Vertical=�6=   $��10q�6T   .+�5S      �� $ 
Horizontal=�6)   .+�5T   "   �� $��Số điện thoại ��6,   #��[ 6B   6f   �� 5U   "   $��*��6,   
���  ��#��[ 6F   6j   5V      �� 5W      -   5         ��Name$ PhoneTB��5W   ]   ���#505B6A$��20(�6<   
�������$��0 5 0 0 q�6n   $	��250��6�   $
��Left=�6�   ��6�   �� *CTNhanVien.SoDienThoaiNV+ . $ PropertyChanged=�.��6  5X      5Y      �� 5Z      $
��Left=�6%   $ Vertical=�6;   $��
10 0 10 10q�6R   .+�5[      �� $ 
Horizontal=�6)   .+�5\   "   �� $��Địa chỉ ��6,   #��[ 6<   6`   �� 5]   "   $��*��6,   
���  ��#��[ 6F   6j   5^      �� 5_      ���#505B6A$��20(�6<   
�������$��0 5 0 10q�6�   $	��200��6�   $
��Left=�6�   ��6]   �� *CTNhanVien.DiaChiNV+ . $ PropertyChanged=�.��6   5`      5a      �� 5b      $
��Left=�6%   $ Vertical=�6;   $��
10 0 10 10q�6R   .+�5c      �� $ 
Horizontal=�6)   .+�5e   "   �� $��Email ��6,   #��[ 6:   6^   �� 5f   "   $��*��6,   
���  ��#��[ 6F   6j   5g      �� 5h      ���#505B6A$��20(�6=   
�������$��0 5 0 10q�6�   $	��200��6�   $
��Left=�6�   ��6^   �� *CTNhanVien.EmailNV+ . $ PropertyChanged=�.��6   5i      5j      5k      5l      �� 5m      $��0 0 0 2q�6   $��Right=�6.   $��Bottom=�6J   $ 
Horizontal=�6b   .+�5n      �� $��0 0 10 0q�6   $��Right=�60   $ 10��6G   $��45��6Y    5o        5p      $ MouseLeftButtonDown��6-   . 5q   "      68   �� *XacNhanCommand+ 6[   �� $ InfoStaffWD��5r   #   5s      ��5u      �� 5v      "
 ��6$   .�5w   "   ��  � �� �#04A2B36Q    5x   "   P� 5y   &    �@J�IsMouseOver . . �5z   *   ��  � �� �#038B9A6Y   5{   '   5}      ._�5      �� $ 	15 2 15 0q�6$   
�������$��Thay đổi��6L   $��Center=�6\   $��Center=�6w   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      �� 5�      $��0 0 20 0q�6   $��Right=�60   $ 10��6G   $��45��6Z    5�        5�      $ MouseLeftButtonDown��6-   . 5�   "      68   �� *HuyBoCommand+ 5�   8   �� $ InfoStaffWD��5�   #   5�      ��5�      �� 5�      "
 ��6$   .�5�   "   ��  � �� �#F8F9FA6Q    5�   "   P� 5�   &    �@J�IsMouseOver . . �5�   *   ��  � �� �#038B9A6Y   5�   '   5�      ._�5�      �� $ 	15 2 15 0q�6$   ���#212529$��
Hủy bỏ��6N   $��Center=�6\   $��Center=�6w   $
��auto��5�   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5�      5�      5�      5�      5�      5�      5�      