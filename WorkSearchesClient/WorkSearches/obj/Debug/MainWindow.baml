   M S B A M L   `   `   `  ����G  CWorkSearches, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WorkSearchesClientWorkSearchesClient  5   	   #    WorkSearchesClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WorkSearchesClient     5   	     � Title$  WorkSearchesClient��5      $	��450��6!   $	��800��6.   .��5	      �� $��	-1,2,-1,5q�6   .@�5
   
   �� $��Вакансии��6   $��-2,-2,-85,0q�6$   .��5      � ���	#FFE5E5E5.+�5      �� $��-3,-5,1,-11q�6   .@�5      �� $!��Поиск вакансий��6   .��5      � ���	#FFE5E5E5.+�5      � $��
-8,35,-8,2q�6#   .+�5   "   &   System.Windows.Controls.DataGrid  -   5        $��CompanyDataGrid��5   +   $��Disabled=�6B      RowDetailsTemplate 5   &   �� 5   *   .R�5   .   �� $��10q�6Z   $
��Wrap=�6f   ��68   ��  �� Path$ Description�6f   5   +   5   #   5      �� 5      $
��Left=�6&   $��23��6A   $��7,7,0,0q�6M    �� TextWrapping$
 Wrap=�6^   $��TextBox��6r   $	��Top=�6�   $	��170��6�   �� 5      -   5        $��
Поиск��5   %   $
��Left=�65   $��	232,5,0,0q�6P   $	��Top=�6c   $��80��6{   $��25��6�   6�   �� 5      -   5        $��CompanySearchTextBox��5   &   $
��Left=�6B   $��23��6]   $��7,7,0,0q�6i   $
 Wrap=�6z   $�� ��6�   $	��Top=�6�   $	��170��6�   �� 5      -   5        $��CompanySearchLabel��5   $   $�� ��6>   $��
343,5,24,0q�6I   $	��Top=�6]   $��25��6u   5      5      �� 5      $'��!Добавить вакансию��6   .��5       � ���	#FFE5E5E5.+�5!      �� -   5        $'��!Добавить вакансию��5!   %   $
��Left=�6A   $��	33,16,0,0q�6\   $	��Top=�6o   $	��120��6�   $��60��6�   6�   �� 5"      -   5        $��AddCompanyNameTextBox��5"   &   $
��Left=�6C   $��25��6^   $��
203,31,0,0q�6j   $
 Wrap=�6~   $'��!Название компании��6�   $	��Top=�6�   $	��211��6�   �� 5#      -   5        $��AddCompanyVacancyTextBox��5#   &   $
��Left=�6F   $��25��6a   $��
438,31,0,0q�6m   $
 Wrap=�6�   $��Вакансия��6�   $	��Top=�6�   $	��178��6�   �� 5$      -   5        $��AddCompanySalaryTextBox��5$   &   $
��Left=�6E   $��25��6`   $��
636,31,0,0q�6l   $
 Wrap=�6�   $��Зарплата��6�   $	��Top=�6�   $	��126��6�   �� 5%      -	   5        $��AddCompanyLabel��5%   $   $�� ��6;   $
��Left=�6F   $��
33,326,0,0q�6a   $	��Top=�6u   $��25��6�   $	��729��6�   �� 5&      -
   5         ��Name$! AddCompanyEmploymentTextBox��5&   &   $
��Left=�6K   $��25��6f   $��
203,70,0,0q�6r   $
 Wrap=�6�   $��Занятость��6�   $	��Top=�6�   $	��211��6�   �� 5'      -   5        $# AddCompanyRequirementsTextBox��5'   &   $
��Left=�6M   $��58��6h   $��
33,113,0,0q�6t   $
 Wrap=�6�   $��Требования��6�   $	��Top=�6�   $	��701��6�   �� 5(      -   5        $" AddCompanyDescriptionTextBox��5(   &   $
��Left=�6L   $	��131��6g   $��
33,176,0,0q�6t   $
 Wrap=�6�   $��Описание��6�   $	��Top=�6�   $	��701��6�   5)      5*      �� 5+      $%��Удалить вакансию��6   $��	1,0,-5,-2q�68   .��5,      � ���	#FFE5E5E5.+�5-      �� -   5        $%��Удалить вакансию��5-   %   $
��Left=�6@   $��	26,10,0,0q�6[   $	��Top=�6n   $	��120��6�   $��60��6�   6�   �� 5.      -   5        $��DelCompanyIdTextBox��5.   &   $
��Left=�6A   $��25��6\   $��
193,31,0,0q�6h   $
 Wrap=�6|   $��Id��6�   $	��Top=�6�   $��70��6�   �� 5/      -   5        $��DelCompanyLabel��5/   $   $�� ��6;   $
��Left=�6F   $��	26,91,0,0q�6a   $	��Top=�6t   $	��739��6�   50      52      �� 53      $'��!Изменить вакансию��6   $��	2,0,-6,-2q�69   .��54      � ���	#FFE5E5E5.+�55      �� -   5        $'��!Изменить вакансию��55   %   $
��Left=�6A   $��	24,30,0,0q�6\   $	��Top=�6o   $	��120��6�   $��60��6�   6�   �� 56      -   5        $��ChangeCompanyId��56   &   $
��Left=�6=   $��25��6X   $��
205,30,0,0q�6d   $
 Wrap=�6x   $��Id��6�   $	��Top=�6�   $��50��6�   �� 57      -   5        $��ChangeCompanyNameTextBox��57   &   $
��Left=�6F   $��25��6a   $��
274,30,0,0q�6m   $
 Wrap=�6�   $'��!Название компании��6�   $	��Top=�6�   $	��155��6�   �� 58      -   5        $!��ChangeCompanyVacancyTextBox��58   &   $
��Left=�6I   $��25��6d   $��
446,30,0,0q�6p   $
 Wrap=�6�   $��Вакансия��6�   $	��Top=�6�   $	��147��6�   �� 59      -   5        $ ��ChangeCompanySalaryTextBox��59   &   $
��Left=�6H   $��25��6c   $��
619,30,0,0q�6o   $
 Wrap=�6�   $��Зарплата��6�   $	��Top=�6�   $	��139��6�   �� 5:      -   5        $��ChangeCompanyLabel��5:   $   $�� ��6>   $
��Left=�6I   $��
24,336,0,0q�6d   $	��Top=�6x   $	��734��6�   �� 5;      -   5        $$ ChangeCompanyEmploymentTextBox��5;   &   $
��Left=�6N   $��25��6i   $��
205,76,0,0q�6u   $
 Wrap=�6�   $��Занятость��6�   $	��Top=�6�   $	��224��6�   �� 5<      -   5        $&  ChangeCompanyRequirementsTextBox��5<   &   $
��Left=�6P   $��58��6k   $��
33,113,0,0q�6w   $
 Wrap=�6�   $��Требования��6�   $	��Top=�6�   $	��701��6�   �� 5=      -   5        $% ChangeCompanyDescriptionTextBox��5=   &   $
��Left=�6O   $	��131��6j   $��
33,176,0,0q�6w   $
 Wrap=�6�   $��Описание��6�   $	��Top=�6�   $	��701��6�   5>      5?      5@      5A      5B      �� 5C   
   $��Соискатели��6   $��83,-2,-165,0q�6&   .��5D      � ���	#FFE5E5E5.+�5E      �� $��-3,-5,1,-11q�6   .@�5F      �� $'��!Поиск соискателей��6   .��5G      � ���	#FFE5E5E5.+�5H      � $��
-8,35,-8,2q�6#   .+�5I   "     -   5          Name$ SeekerDataGrid��5I   +   $��Disabled=�6C    5J   &   �� 5K   *   .R�5L   .   �� $��10q�6Z   $
��Wrap=�6f   ��68   �� $ Description�6f   5M   +   5O   #   5P      �� 5Q      -   5        $ SeekerSearchTextBox��5Q   &   $
��Left=�6C   $��23��6^   $��7,7,0,0q�6j   $
 Wrap=�6{   $�� ��6�   $	��Top=�6�   $	��170��6�   �� 5R      -   5        $��
Поиск��5R   %   $
��Left=�65   $��	232,5,0,0q�6P   $	��Top=�6c   $��80��6{   $��25��6�   6�   �� 5S      -   5         ��Name$ SeekerSearchLabel��5S   $   $�� ��6?   $
��Left=�6J   $��
344,10,0,0q�6e   $	��Top=�6y   $��25��6�   $	��417��6�   5T      5U      �� 5V      $+��%Добавить соискателя��6   .��5W      � ���	#FFE5E5E5.+�5X      �� -   5        $+��%Добавить соискателя��5X   %   $
��Left=�6C   $��	33,16,0,0q�6^   $	��Top=�6q   $	��133��6�   $��60��6�   6�   �� 5Y      -   5        $ AddSeekerNameTextBox��5Y   &   $
��Left=�6D   $��25��6_   $��
203,31,0,0q�6k   $
 Wrap=�6   $��Имя��6�   $	��Top=�6�   $	��211��6�   �� 5Z      -   5        $ AddSeekerVacancyTextBox��5Z   &   $
��Left=�6G   $��25��6b   $��
438,31,0,0q�6n   $
 Wrap=�6�   $'��!Желаемая вакансия��6�   $	��Top=�6�   $	��178��6�   �� 5[      -    5        $ AddSeekerSalaryTextBox��5[   &   $
��Left=�6F   $��25��6a   $��
636,31,0,0q�6m   $
 Wrap=�6�   $'��!Желаемая зарплата��6�   $	��Top=�6�   $	��126��6�   �� 5\      -!   5        $ AddSeekerEducationTextBox��5\   &   $
��Left=�6I   $��25��6d   $��
203,70,0,0q�6p   $
 Wrap=�6�   $��Образование��6�   $	��Top=�6�   $	��211��6�   �� 5]      -"   5        $" AddSeekerMobileNumberTextBox��5]   &   $
��Left=�6L   $��25��6g   $��
438,70,0,0q�6s   $
 Wrap=�6�   $#��Мобильный номер��6�   $	��Top=�6�   $	��324��6�   �� 5^      -#   5        $! AddSeekerDescriptionTextBox��5^   &   $
��Left=�6K   $	��186��6f   $��
33,111,0,0q�6s   $
 Wrap=�6�   $��Описание��6�   $	��Top=�6�   $	��701��6�   �� 5_      -$   5        $ AddSeekerLabel��5_   $   $�� ��6<   $
��Left=�6G   $��
33,326,0,0q�6b   $	��Top=�6v   $��25��6�   $	��729��6�   5`      5a      �� 5b      $)��#Удалить соискателя��6   .��5c      � ���	#FFE5E5E5.+�5d      �� -%   5        $)��#Удалить соискателя��5d   %   $
��Left=�6B   $��	26,10,0,0q�6]   $	��Top=�6p   $	��120��6�   $��60��6�   6�   �� 5e      -&   5        $ DelSeekerIdTextBox��5e   &   $
��Left=�6B   $��25��6]   $��
193,31,0,0q�6i   $
 Wrap=�6}   $��Id��6�   $	��Top=�6�   $��70��6�   �� 5f      -'   5        $ DelSeekerLabel��5f   $   $�� ��6<   $
��Left=�6G   $��	26,91,0,0q�6b   $	��Top=�6u   $	��739��6�   5g      5h      �� 5i      $+��%Изменить соискателя��6   $��	1,0,-5,-2q�6;   .��5j      � ���	#FFE5E5E5.+�5k      �� -(   5        $8��2Изменить анкету соискателя��5k   %   $
��Left=�6J   $��	10,16,0,0q�6e   $	��Top=�6x   $	��178��6�   $��60��6�   6�   �� 5l      -)   5        $ ChangeSeekerNameTextBox��5l   &   $
��Left=�6G   $��25��6b   $��
342,31,0,0q�6n   $
 Wrap=�6�   $��Имя��6�   $	��Top=�6�   $	��211��6�   �� 5m      -*   5        $ ChangeSeekerIdTextBox��5m   &   $
��Left=�6E   $��25��6`   $��
210,31,0,0q�6l   $
 Wrap=�6�   $��Id��6�   $	��Top=�6�   $��70��6�   �� 5n      -+   5        $  ChangeSeekerVacancyTextBox��5n   &   $
��Left=�6J   $��25��6e   $��
579,31,0,0q�6q   $
 Wrap=�6�   $'��!Желаемая вакансия��6�   $	��Top=�6�   $	��178��6�   �� 5o      -,   5        $ ChangeSeekerSalaryTextBox��5o   &   $
��Left=�6I   $��25��6d   $��210,108,0,0q�6p   $
 Wrap=�6�   $'��!Желаемая зарплата��6�   $	��Top=�6�   $	��133��6�   �� 5p      --   5        $" ChangeSeekerEducationTextBox��5p   &   $
��Left=�6L   $��25��6g   $��
210,72,0,0q�6s   $
 Wrap=�6�   $��Образование��6�   $	��Top=�6�   $	��211��6�   �� 5q      -.   5        $% ChangeSeekerMobileNumberTextBox��5q   &   $
��Left=�6O   $��25��6j   $��
468,72,0,0q�6v   $
 Wrap=�6�   $#��Мобильный номер��6�   $	��Top=�6�   $	��289��6�   �� 5r      -/   5        $$ ChangeSeekerDescriptionTextBox��5r   &   $
��Left=�6N   $	��186��6i   $��
40,138,0,0q�6v   $
 Wrap=�6�   $��Описание��6�   $	��Top=�6�   $	��701��6�   �� 5s      -0   5        $ ChangeSeekerLabel��5s   $   $�� ��6?   $
��Left=�6J   $��
24,336,0,0q�6e   $	��Top=�6y   $	��734��6�   5t      5u      5v      5w      5z      5{      5|      