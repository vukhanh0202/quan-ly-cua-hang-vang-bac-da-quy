   M S B A M L   `   `   `  ����R  NGemstonesBusinessSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GemstonesBusinessSystemGemstonesBusinessSystem  5   	   .    (GemstonesBusinessSystem.NewServiceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   1local%clr-namespace:GemstonesBusinessSystem  5   	   -   5        $��
NewService��5
   	      NewServiceVM  � DataContext#  [  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ NewServiceWindow��5   	   $	��550��6"   $	��750��6/   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ 
NewService��5      	 � 	Resources	 5      
 �� 
TargetType  tblTitle �� Property �� Value��%}   &	       �� 5   
   "
 ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ 22(�6)   66   5      5      .��5      ]� ��. ���#2A323C.+�5   
   � $��60��6   $	��Top=�6   ���#262626+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6F   .+�5      �� $��Center=�6    �� Padding$ 20 5 0 0q�63   $��Thêm dịch vụ��6F   
�������$��25(�6m   $��DemiBold"�6{   6�   5      � 5   
   $	��Top=�6   ���#2A323C$��0 0 0 10q�6:   .+�5!      ]� ��. .+�5"      ��  �� CornerRadius$ 15��6   $	��Top=�6+   ���#323C48$��30 40 30 10q�6U   ._�5#      ]� $��40 30 40 40q�6    .+�5$      �� $	��Top=�6$   $��Dịch vụ��69   
�������$��30(�6[   $��	20 20 0 0q�6i   $��Medium"�6|   6�   �� 5%      $	��Top=�6%    �� Orientation$ Vertical=�6:   $��20 10q�6Q   .+�5&      �� $ 
Horizontal=�6)   .+�5'   "   �� $��Tên dịch vụ ��6,   #��[ 6@   6d   �� 5(   "   $��*��6,   
���  ��#��[ 6F   6j   5)      �� 5*      ���#505B6A$��0 5 0 0q�6<   $��20(�6M   
���������6n   �� *DichVuMoi.TenDichVu+ �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�.��6�   5+      5,      ]� 5-      $	��Top=�6$   .+�5.      �� $
��Left=�6)   $ Vertical=�6?   $��20 10q�6V   .+�5/   "   �� $ 
Horizontal=�6-   .+�50   &   �� $��
Đơn giá��60   #��[ 6?   6c   �� 51   &   $��*��60   
���  ��#��[ 6J   6n   52   #   �� 53   "   ���#505B6A$��20(�6@   
�������$��0 5 0 10q�6�   $	��200��6  $
��Left=�6  ��6a   �� *DichVuMoi.DonGiaDV+ �� StringFormat$ 	{0:#,##0}�� �� ConverterCulture$ vi-VN�� . $ PropertyChanged=�.��67  54   #   55      57      59      5:      �� 5;      $��	20 0 10 0q�6   $��Right=�60   $��Bottom=�6L   $ 
Horizontal=�6d   .+�5<      �� $��0 0 10 0q�6   $��Right=�60   $ 10��6G   $��45��6Y    5=        5>      $ MouseLeftButtonDown��6-   . 5?   "      68   �� *XacNhanCommand+ 6[   �� $ 
NewService��5@   #   5A      ��5C      �� 5D      "
 ��6$   .�5E   "   ��  � �� �#04A2B36Q    �� Triggers 5F   "   P� 5G   &    P� Property �@J�IsMouseOver P� Value . . �5H   *   ��  � �� �#038B9A6Y   5I   '   5K      ._�5M      �� $ 	15 2 15 0q�6$   
�������$��Xác nhận��6L   $��Center=�6\   $��Center=�6w   $
��auto��5N   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5O      5P      �� 5Q      $��0 0 20 0q�6   $��Right=�60   $ 10��6G   $��45��6Z    5R        5S      $ MouseLeftButtonDown��6-   . 5T   "      68   �� *HuyBoCommand+ 5U   8   �� $ 
NewService��5V   #   5W      ��5Y      �� 5Z      "
 ��6$   .�5[   "   ��  � �� �#F8F9FA6Q    5\   "   P� 5]   &    �@J�IsMouseOver . . �5^   *   ��  � �� �#E2E6EA6Y   5_   '   5a      ._�5c      �� $ 	15 2 15 0q�6$   ���#212529$��
Hủy bỏ��6N   $��Center=�6\   $��Center=�6w   $
��auto��5d   +   $��25(�69   $A��;/GemstonesBusinessSystem;component/fonts/#Baloo Da 2 Medium)�6G   5e      5f      5g      5i      5j      5k      5l      