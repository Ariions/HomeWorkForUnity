Kadangi nelabai isejo padaryti pure ECS kaip buvau uzsibrezes is pradziu, perejau prie hybrid ecs 
viskas atlikta su Unity 2018.2.10f1

all samples are launched with samplescenes or builds
Note: Pure - ECS doesnt have collition or color changing like others two
Note: profiliavau editoriuje

No ECS or optimization:
Freametime: 205 ms
memory: 890MB

Hybrid ECS no optimizations: just rewrote everything to use ecs

Frametime: 54ms
memory: 270MB


Hybrid ECS some optimizations: tried adding logic into one file my guess that grouping sprite renderer component into memory was a bad idea because majority of 

Frametime: 64ms
memory: ~270MB


Hybrid ECS some optimizations and fixed collition:

Frametime: 60 ms
memory: ~240 MB